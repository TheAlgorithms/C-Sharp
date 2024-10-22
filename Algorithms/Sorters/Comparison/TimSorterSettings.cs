namespace Algorithms.Sorters.Comparison;

public class TimSorterSettings
{
    public int MinMerge { get; }

    public int MinGallop { get; }

    public TimSorterSettings(int minMerge = 32, int minGallop = 7)
    {
        MinMerge = minMerge;
        MinGallop = minGallop;
    }
}
