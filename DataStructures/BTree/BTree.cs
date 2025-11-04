namespace DataStructures.BTree;

/// <summary>
///     A self-balancing search tree data structure that maintains sorted data
///     and allows searches, sequential access, insertions, and deletions in
///     logarithmic time.
/// </summary>
/// <remarks>
///     A B-Tree is a generalization of a binary search tree in which a node
///     can have more than two children. Unlike self-balancing binary search
///     trees, the B-Tree is optimized for systems that read and write large
///     blocks of data. It is commonly used in databases and file systems.
///
///     Properties of a B-Tree of minimum degree t:
///     1. Every node has at most 2t-1 keys.
///     2. Every node (except root) has at least t-1 keys.
///     3. The root has at least 1 key (if tree is not empty).
///     4. All leaves are at the same level.
///     5. A non-leaf node with k keys has k+1 children.
///
///     Time Complexity:
///     - Search: O(log n)
///     - Insert: O(log n)
///     - Delete: O(log n)
///
///     Space Complexity: O(n)
///
///     See https://en.wikipedia.org/wiki/B-tree for more information.
///     Visualizer: https://www.cs.usfca.edu/~galles/visualization/BTree.html.
/// </remarks>
/// <typeparam name="TKey">Type of key for the tree.</typeparam>
public class BTree<TKey>
{
    /// <summary>
    ///     Gets the number of keys in the tree.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Gets the minimum degree (t) of the B-Tree.
    ///     Each node can contain at most 2t-1 keys and at least t-1 keys (except root).
    /// </summary>
    public int MinimumDegree { get; }

    /// <summary>
    ///     Comparer to use when comparing key values.
    /// </summary>
    private readonly Comparer<TKey> comparer;

    /// <summary>
    ///     Reference to the root node.
    /// </summary>
    private BTreeNode<TKey>? root;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BTree{TKey}"/>
    ///     class with the specified minimum degree.
    /// </summary>
    /// <param name="minimumDegree">
    ///     Minimum degree (t) of the B-Tree. Must be at least 2.
    ///     Each node can contain at most 2t-1 keys.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Thrown when minimumDegree is less than 2.
    /// </exception>
    public BTree(int minimumDegree = 2)
    {
        if (minimumDegree < 2)
        {
            throw new ArgumentException("Minimum degree must be at least 2.", nameof(minimumDegree));
        }

        MinimumDegree = minimumDegree;
        comparer = Comparer<TKey>.Default;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BTree{TKey}"/>
    ///     class with the specified minimum degree and custom comparer.
    /// </summary>
    /// <param name="minimumDegree">
    ///     Minimum degree (t) of the B-Tree. Must be at least 2.
    /// </param>
    /// <param name="customComparer">
    ///     Comparer to use when comparing keys.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Thrown when minimumDegree is less than 2.
    /// </exception>
    public BTree(int minimumDegree, Comparer<TKey> customComparer)
    {
        if (minimumDegree < 2)
        {
            throw new ArgumentException("Minimum degree must be at least 2.", nameof(minimumDegree));
        }

        MinimumDegree = minimumDegree;
        comparer = customComparer;
    }

    /// <summary>
    ///     Add a single key to the tree.
    /// </summary>
    /// <param name="key">Key value to add.</param>
    public void Add(TKey key)
    {
        if (root is null)
        {
            root = new BTreeNode<TKey>(MinimumDegree, true);
            root.InsertKey(0, key);
            Count++;
            return;
        }

        if (root.IsFull())
        {
            var newRoot = new BTreeNode<TKey>(MinimumDegree, false);
            newRoot.InsertChild(0, root);
            SplitChild(newRoot, 0);
            root = newRoot;
        }

        InsertNonFull(root, key);
        Count++;
    }

    /// <summary>
    ///     Add multiple keys to the tree.
    /// </summary>
    /// <param name="keys">Key values to add.</param>
    public void AddRange(IEnumerable<TKey> keys)
    {
        foreach (var key in keys)
        {
            Add(key);
        }
    }

    /// <summary>
    ///     Remove a key from the tree.
    /// </summary>
    /// <param name="key">Key value to remove.</param>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown when the key is not found in the tree.
    /// </exception>
    public void Remove(TKey key)
    {
        if (root is null)
        {
            throw new KeyNotFoundException($"""Key "{key}" is not in the B-Tree.""");
        }

        Remove(root, key);

        if (root.KeyCount == 0)
        {
            root = root.IsLeaf ? null : root.Children[0];
        }

        Count--;
    }

    /// <summary>
    ///     Check if given key is in the tree.
    /// </summary>
    /// <param name="key">Key value to search for.</param>
    /// <returns>Whether or not the key is in the tree.</returns>
    public bool Contains(TKey key)
    {
        return Search(root, key) is not null;
    }

    /// <summary>
    ///     Get the minimum key in the tree.
    /// </summary>
    /// <returns>Minimum key in tree.</returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the tree is empty.
    /// </exception>
    public TKey GetMin()
    {
        if (root is null)
        {
            throw new InvalidOperationException("B-Tree is empty.");
        }

        return GetMin(root);
    }

    /// <summary>
    ///     Get the maximum key in the tree.
    /// </summary>
    /// <returns>Maximum key in tree.</returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the tree is empty.
    /// </exception>
    public TKey GetMax()
    {
        if (root is null)
        {
            throw new InvalidOperationException("B-Tree is empty.");
        }

        return GetMax(root);
    }

    /// <summary>
    ///     Get keys in order from smallest to largest as defined by the
    ///     comparer.
    /// </summary>
    /// <returns>Keys in tree in order from smallest to largest.</returns>
    public IEnumerable<TKey> GetKeysInOrder()
    {
        List<TKey> result = [];
        InOrderTraversal(root);
        return result;

        void InOrderTraversal(BTreeNode<TKey>? node)
        {
            if (node is null)
            {
                return;
            }

            for (var i = 0; i < node.KeyCount; i++)
            {
                if (!node.IsLeaf)
                {
                    InOrderTraversal(node.Children[i]);
                }

                result.Add(node.Keys[i]);
            }

            if (!node.IsLeaf)
            {
                InOrderTraversal(node.Children[node.KeyCount]);
            }
        }
    }

    /// <summary>
    ///     Get keys in the pre-order order.
    /// </summary>
    /// <returns>Keys in pre-order order.</returns>
    public IEnumerable<TKey> GetKeysPreOrder()
    {
        var result = new List<TKey>();
        PreOrderTraversal(root);
        return result;

        void PreOrderTraversal(BTreeNode<TKey>? node)
        {
            if (node is null)
            {
                return;
            }

            for (var i = 0; i < node.KeyCount; i++)
            {
                result.Add(node.Keys[i]);
            }

            if (!node.IsLeaf)
            {
                for (var i = 0; i <= node.KeyCount; i++)
                {
                    PreOrderTraversal(node.Children[i]);
                }
            }
        }
    }

    /// <summary>
    ///     Get keys in the post-order order.
    /// </summary>
    /// <returns>Keys in the post-order order.</returns>
    public IEnumerable<TKey> GetKeysPostOrder()
    {
        var result = new List<TKey>();
        PostOrderTraversal(root);
        return result;

        void PostOrderTraversal(BTreeNode<TKey>? node)
        {
            if (node is null)
            {
                return;
            }

            if (!node.IsLeaf)
            {
                for (var i = 0; i <= node.KeyCount; i++)
                {
                    PostOrderTraversal(node.Children[i]);
                }
            }

            for (var i = 0; i < node.KeyCount; i++)
            {
                result.Add(node.Keys[i]);
            }
        }
    }

    /// <summary>
    ///     Search for a key in the subtree rooted at the given node.
    /// </summary>
    /// <param name="node">Root of the subtree to search.</param>
    /// <param name="key">Key to search for.</param>
    /// <returns>Node containing the key, or null if not found.</returns>
    private BTreeNode<TKey>? Search(BTreeNode<TKey>? node, TKey key)
    {
        if (node is null)
        {
            return null;
        }

        var i = 0;
        while (i < node.KeyCount && comparer.Compare(key, node.Keys[i]) > 0)
        {
            i++;
        }

        if (i < node.KeyCount && comparer.Compare(key, node.Keys[i]) == 0)
        {
            return node;
        }

        if (node.IsLeaf)
        {
            return null;
        }

        return Search(node.Children[i], key);
    }

    /// <summary>
    ///     Insert a key into a non-full node.
    /// </summary>
    /// <param name="node">Node to insert the key into.</param>
    /// <param name="key">Key to insert.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when the key already exists in the tree.
    /// </exception>
    private void InsertNonFull(BTreeNode<TKey> node, TKey key)
    {
        if (node.IsLeaf)
        {
            InsertIntoLeaf(node, key);
        }
        else
        {
            InsertIntoNonLeaf(node, key);
        }
    }

    /// <summary>
    ///     Insert a key into a leaf node.
    /// </summary>
    /// <param name="node">Leaf node to insert into.</param>
    /// <param name="key">Key to insert.</param>
    private void InsertIntoLeaf(BTreeNode<TKey> node, TKey key)
    {
        var i = node.KeyCount - 1;

        while (i >= 0 && comparer.Compare(key, node.Keys[i]) < 0)
        {
            node.Keys[i + 1] = node.Keys[i];
            i--;
        }

        if (i >= 0 && comparer.Compare(key, node.Keys[i]) == 0)
        {
            throw new ArgumentException($"""Key "{key}" already exists in B-Tree.""");
        }

        node.Keys[i + 1] = key;
        node.KeyCount++;
    }

    /// <summary>
    ///     Insert a key into a non-leaf node.
    /// </summary>
    /// <param name="node">Non-leaf node to insert into.</param>
    /// <param name="key">Key to insert.</param>
    private void InsertIntoNonLeaf(BTreeNode<TKey> node, TKey key)
    {
        var i = FindInsertionIndex(node, key);

        if (node.Children[i]!.IsFull())
        {
            SplitChild(node, i);

            if (comparer.Compare(key, node.Keys[i]) > 0)
            {
                i++;
            }
        }

        InsertNonFull(node.Children[i]!, key);
    }

    /// <summary>
    ///     Find the index where a key should be inserted in a non-leaf node.
    /// </summary>
    /// <param name="node">Node to search in.</param>
    /// <param name="key">Key to insert.</param>
    /// <returns>Index where the key should be inserted.</returns>
    private int FindInsertionIndex(BTreeNode<TKey> node, TKey key)
    {
        var i = node.KeyCount - 1;

        while (i >= 0 && comparer.Compare(key, node.Keys[i]) < 0)
        {
            i--;
        }

        if (i >= 0 && comparer.Compare(key, node.Keys[i]) == 0)
        {
            throw new ArgumentException($"""Key "{key}" already exists in B-Tree.""");
        }

        return i + 1;
    }

    /// <summary>
    ///     Split a full child of a node.
    /// </summary>
    /// <param name="parent">Parent node.</param>
    /// <param name="childIndex">Index of the child to split.</param>
    private void SplitChild(BTreeNode<TKey> parent, int childIndex)
    {
        var fullChild = parent.Children[childIndex]!;
        var newChild = new BTreeNode<TKey>(MinimumDegree, fullChild.IsLeaf);
        var midIndex = MinimumDegree - 1;

        for (var j = 0; j < MinimumDegree - 1; j++)
        {
            newChild.Keys[j] = fullChild.Keys[j + MinimumDegree];
            newChild.KeyCount++;
        }

        if (!fullChild.IsLeaf)
        {
            for (var j = 0; j < MinimumDegree; j++)
            {
                newChild.Children[j] = fullChild.Children[j + MinimumDegree];
            }
        }

        fullChild.KeyCount = MinimumDegree - 1;

        for (var j = parent.KeyCount; j > childIndex; j--)
        {
            parent.Children[j + 1] = parent.Children[j];
        }

        parent.Children[childIndex + 1] = newChild;

        for (var j = parent.KeyCount - 1; j >= childIndex; j--)
        {
            parent.Keys[j + 1] = parent.Keys[j];
        }

        parent.Keys[childIndex] = fullChild.Keys[midIndex];
        parent.KeyCount++;
    }

    /// <summary>
    ///     Remove a key from the subtree rooted at the given node.
    /// </summary>
    /// <param name="node">Root of the subtree.</param>
    /// <param name="key">Key to remove.</param>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown when the key is not found in the subtree.
    /// </exception>
    private void Remove(BTreeNode<TKey> node, TKey key)
    {
        var idx = FindKey(node, key);

        if (idx < node.KeyCount && comparer.Compare(node.Keys[idx], key) == 0)
        {
            if (node.IsLeaf)
            {
                RemoveFromLeaf(node, idx);
            }
            else
            {
                RemoveFromNonLeaf(node, idx);
            }
        }
        else
        {
            if (node.IsLeaf)
            {
                throw new KeyNotFoundException($"""Key "{key}" is not in the B-Tree.""");
            }

            var isInSubtree = idx == node.KeyCount;

            if (node.Children[idx]!.KeyCount < MinimumDegree)
            {
                Fill(node, idx);
            }

            if (isInSubtree && idx > node.KeyCount)
            {
                Remove(node.Children[idx - 1]!, key);
            }
            else
            {
                Remove(node.Children[idx]!, key);
            }
        }
    }

    /// <summary>
    ///     Find the index of the first key greater than or equal to the given key.
    /// </summary>
    /// <param name="node">Node to search in.</param>
    /// <param name="key">Key to find.</param>
    /// <returns>Index of the first key >= key.</returns>
    private int FindKey(BTreeNode<TKey> node, TKey key)
    {
        var idx = 0;
        while (idx < node.KeyCount && comparer.Compare(node.Keys[idx], key) < 0)
        {
            idx++;
        }

        return idx;
    }

    /// <summary>
    ///     Remove a key from a leaf node.
    /// </summary>
    /// <param name="node">Leaf node to remove from.</param>
    /// <param name="idx">Index of the key to remove.</param>
    private void RemoveFromLeaf(BTreeNode<TKey> node, int idx)
    {
        node.RemoveKey(idx);
    }

    /// <summary>
    ///     Remove a key from a non-leaf node.
    /// </summary>
    /// <param name="node">Non-leaf node to remove from.</param>
    /// <param name="idx">Index of the key to remove.</param>
    private void RemoveFromNonLeaf(BTreeNode<TKey> node, int idx)
    {
        var key = node.Keys[idx];

        if (node.Children[idx]!.KeyCount >= MinimumDegree)
        {
            var predecessor = GetPredecessor(node, idx);
            node.Keys[idx] = predecessor;
            Remove(node.Children[idx]!, predecessor);
        }
        else if (node.Children[idx + 1]!.KeyCount >= MinimumDegree)
        {
            var successor = GetSuccessor(node, idx);
            node.Keys[idx] = successor;
            Remove(node.Children[idx + 1]!, successor);
        }
        else
        {
            Merge(node, idx);
            Remove(node.Children[idx]!, key);
        }
    }

    /// <summary>
    ///     Get the predecessor key of the key at the given index.
    /// </summary>
    /// <param name="node">Node containing the key.</param>
    /// <param name="idx">Index of the key.</param>
    /// <returns>Predecessor key.</returns>
    private TKey GetPredecessor(BTreeNode<TKey> node, int idx)
    {
        var current = node.Children[idx]!;
        while (!current.IsLeaf)
        {
            current = current.Children[current.KeyCount]!;
        }

        return current.Keys[current.KeyCount - 1];
    }

    /// <summary>
    ///     Get the successor key of the key at the given index.
    /// </summary>
    /// <param name="node">Node containing the key.</param>
    /// <param name="idx">Index of the key.</param>
    /// <returns>Successor key.</returns>
    private TKey GetSuccessor(BTreeNode<TKey> node, int idx)
    {
        var current = node.Children[idx + 1]!;
        while (!current.IsLeaf)
        {
            current = current.Children[0]!;
        }

        return current.Keys[0];
    }

    /// <summary>
    ///     Fill a child node that has fewer than minimum degree - 1 keys.
    /// </summary>
    /// <param name="node">Parent node.</param>
    /// <param name="idx">Index of the child to fill.</param>
    private void Fill(BTreeNode<TKey> node, int idx)
    {
        if (idx != 0 && node.Children[idx - 1]!.KeyCount >= MinimumDegree)
        {
            BorrowFromPrevious(node, idx);
        }
        else if (idx != node.KeyCount && node.Children[idx + 1]!.KeyCount >= MinimumDegree)
        {
            BorrowFromNext(node, idx);
        }
        else
        {
            Merge(node, idx != node.KeyCount ? idx : idx - 1);
        }
    }

    /// <summary>
    ///     Borrow a key from the previous sibling.
    /// </summary>
    /// <param name="node">Parent node.</param>
    /// <param name="childIdx">Index of the child that needs a key.</param>
    private void BorrowFromPrevious(BTreeNode<TKey> node, int childIdx)
    {
        var child = node.Children[childIdx]!;
        var sibling = node.Children[childIdx - 1]!;

        for (var i = child.KeyCount - 1; i >= 0; i--)
        {
            child.Keys[i + 1] = child.Keys[i];
        }

        if (!child.IsLeaf)
        {
            for (var i = child.KeyCount; i >= 0; i--)
            {
                child.Children[i + 1] = child.Children[i];
            }
        }

        child.Keys[0] = node.Keys[childIdx - 1];

        if (!child.IsLeaf)
        {
            child.Children[0] = sibling.Children[sibling.KeyCount];
        }

        node.Keys[childIdx - 1] = sibling.Keys[sibling.KeyCount - 1];

        child.KeyCount++;
        sibling.KeyCount--;
    }

    /// <summary>
    ///     Borrow a key from the next sibling.
    /// </summary>
    /// <param name="node">Parent node.</param>
    /// <param name="childIdx">Index of the child that needs a key.</param>
    private void BorrowFromNext(BTreeNode<TKey> node, int childIdx)
    {
        var child = node.Children[childIdx]!;
        var sibling = node.Children[childIdx + 1]!;

        child.Keys[child.KeyCount] = node.Keys[childIdx];

        if (!child.IsLeaf)
        {
            child.Children[child.KeyCount + 1] = sibling.Children[0];
        }

        node.Keys[childIdx] = sibling.Keys[0];

        for (var i = 1; i < sibling.KeyCount; i++)
        {
            sibling.Keys[i - 1] = sibling.Keys[i];
        }

        if (!sibling.IsLeaf)
        {
            for (var i = 1; i <= sibling.KeyCount; i++)
            {
                sibling.Children[i - 1] = sibling.Children[i];
            }
        }

        child.KeyCount++;
        sibling.KeyCount--;
    }

    /// <summary>
    ///     Merge a child with its sibling.
    /// </summary>
    /// <param name="node">Parent node.</param>
    /// <param name="idx">Index of the child to merge.</param>
    private void Merge(BTreeNode<TKey> node, int idx)
    {
        var child = node.Children[idx]!;
        var sibling = node.Children[idx + 1]!;

        child.Keys[MinimumDegree - 1] = node.Keys[idx];

        for (var i = 0; i < sibling.KeyCount; i++)
        {
            child.Keys[i + MinimumDegree] = sibling.Keys[i];
        }

        if (!child.IsLeaf)
        {
            for (var i = 0; i <= sibling.KeyCount; i++)
            {
                child.Children[i + MinimumDegree] = sibling.Children[i];
            }
        }

        for (var i = idx + 1; i < node.KeyCount; i++)
        {
            node.Keys[i - 1] = node.Keys[i];
        }

        for (var i = idx + 2; i <= node.KeyCount; i++)
        {
            node.Children[i - 1] = node.Children[i];
        }

        child.KeyCount += sibling.KeyCount + 1;
        node.KeyCount--;
    }

    /// <summary>
    ///     Get the minimum key in the subtree rooted at the given node.
    /// </summary>
    /// <param name="node">Root of the subtree.</param>
    /// <returns>Minimum key in the subtree.</returns>
    private TKey GetMin(BTreeNode<TKey> node)
    {
        while (!node.IsLeaf)
        {
            node = node.Children[0]!;
        }

        return node.Keys[0];
    }

    /// <summary>
    ///     Get the maximum key in the subtree rooted at the given node.
    /// </summary>
    /// <param name="node">Root of the subtree.</param>
    /// <returns>Maximum key in the subtree.</returns>
    private TKey GetMax(BTreeNode<TKey> node)
    {
        while (!node.IsLeaf)
        {
            node = node.Children[node.KeyCount]!;
        }

        return node.Keys[node.KeyCount - 1];
    }
}
