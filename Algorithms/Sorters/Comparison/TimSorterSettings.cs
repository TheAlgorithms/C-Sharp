namespace Algorithms.Sorters.Comparison;

public class TimSorterSettings(int minMerge = 32, int minGallop = 7)
{
    public int MinMerge { get; } = minMerge;

    public int MinGallop { get; } = minGallop;
}
