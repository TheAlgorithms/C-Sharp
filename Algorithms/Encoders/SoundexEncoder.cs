using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Encoders;

/// <summary>
///     Class for Soundex encoding strings.
/// </summary>
public class SoundexEncoder
{
    /// <summary>
    ///     Encodes a string using the Soundex Algorithm.
    /// </summary>
    /// <param name="text">The string to encode.</param>
    /// <returns>The Soundex encoded string (one uppercase character and three digits).</returns>
    public string Encode(string text)
    {
        text = text.ToLowerInvariant();
        var chars = OmitHAndW(text);
        IEnumerable<int> numbers = ProduceNumberCoding(chars);
        numbers = CollapseDoubles(numbers);
        numbers = OmitVowels(numbers);
        numbers = CollapseLeadingDigit(numbers, text[0]);
        numbers = numbers.Take(3);
        numbers = PadTo3Numbers(numbers);
        var final = numbers.ToArray();
        return $"{text.ToUpperInvariant()[0]}{final[0]}{final[1]}{final[2]}";
    }

    private IEnumerable<int> CollapseLeadingDigit(IEnumerable<int> numbers, char c)
    {
        using var enumerator = numbers.GetEnumerator();
        enumerator.MoveNext();
        if (enumerator.Current == MapToNumber(c))
        {
            enumerator.MoveNext();
        }

        do
        {
            yield return enumerator.Current;
        }
        while (enumerator.MoveNext());
    }

    private IEnumerable<int> PadTo3Numbers(IEnumerable<int> numbers)
    {
        using var enumerator = numbers.GetEnumerator();
        for (var i = 0; i < 3; i++)
        {
            yield return enumerator.MoveNext()
                ? enumerator.Current
                : 0;
        }
    }

    private IEnumerable<int> OmitVowels(IEnumerable<int> numbers) => numbers.Where(i => i != 0);

    private IEnumerable<char> OmitHAndW(string text) => text.Where(c => c != 'h' && c != 'w');

    private IEnumerable<int> CollapseDoubles(IEnumerable<int> numbers)
    {
        var previous = int.MinValue;
        foreach (var i in numbers)
        {
            if (previous != i)
            {
                yield return i;
                previous = i;
            }
        }
    }

    private IEnumerable<int> ProduceNumberCoding(IEnumerable<char> text) => text.Select(MapToNumber);

    private int MapToNumber(char ch)
    {
        var mapping = new Dictionary<char, int>
        {
            ['a'] = 0,
            ['e'] = 0,
            ['i'] = 0,
            ['o'] = 0,
            ['u'] = 0,
            ['y'] = 0,
            ['h'] = 8,
            ['w'] = 8,
            ['b'] = 1,
            ['f'] = 1,
            ['p'] = 1,
            ['v'] = 1,
            ['c'] = 2,
            ['g'] = 2,
            ['j'] = 2,
            ['k'] = 2,
            ['q'] = 2,
            ['s'] = 2,
            ['x'] = 2,
            ['z'] = 2,
            ['d'] = 3,
            ['t'] = 3,
            ['l'] = 4,
            ['m'] = 5,
            ['n'] = 5,
            ['r'] = 6,
        };

        return mapping[ch];
    }
}
