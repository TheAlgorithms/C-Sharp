using System.Collections.Generic;

public class LFUCache
{
    private int _minCount;
    private readonly int _capacity;
    private readonly Dictionary<int, (LinkedListNode<int> node, int value, int count)> _cache;
    private readonly Dictionary<int, LinkedList<int>> _countMap;

    public LFUCache(int capacity)
    {
        _capacity = capacity;
        _countMap = new Dictionary<int, LinkedList<int>> { [1] = new() };
        _cache = new Dictionary<int, (LinkedListNode<int> node, int value, int count)>(capacity);
    }

    public int Get(int key)
    {
        if (_capacity <= 0 || !_cache.ContainsKey(key))
            return -1;

        var (node, value, count) = _cache[key];
        PromoteItem(key, value, count, node);

        return value;
    }

    public void Put(int key, int value)
    {
        if (_capacity <= 0) return;

        if (_cache.ContainsKey(key))
        {
            var (node, _, count) = _cache[key];
            PromoteItem(key, value, count, node);
        }
        else
        {
            if (_cache.Count >= _capacity)
            {
                var minList = _countMap[_minCount];
                _cache.Remove(minList.Last!.Value);
                minList.RemoveLast();
            }

            _cache.Add(key, (_countMap[1].AddFirst(key), value, 1));
            _minCount = 1;
        }
    }

    private void PromoteItem(int key, int value, int count, LinkedListNode<int> node)
    {
        var list = _countMap[count];
        list.Remove(node);

        if (_minCount == count && list.Count == 0)
            _minCount++;

        var newCount = count + 1;
        if (!_countMap.ContainsKey(newCount))
            _countMap[newCount] = new LinkedList<int>();

        _countMap[newCount].AddFirst(node);
        _cache[key] = (node, value, newCount);
    }
}