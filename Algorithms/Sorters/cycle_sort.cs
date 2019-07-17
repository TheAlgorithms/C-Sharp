namespace cycle_sort
{
    internal class Program
    {
        public static void CycleSort(int[] data)
        {
            for (var cycleStart = 0; cycleStart <= data.Length - 2; cycleStart++)
            {
                var item = data[cycleStart];
                var pos = cycleStart;
                for (var i = cycleStart + 1; i <= data.Length - 1; i++)
                {
                    if (data[i] < item)
                    {
                        pos++;
                    }
                }

                if (pos == cycleStart)
                {
                    continue;
                }

                while (data[pos] == item)
                {
                    pos++;
                }

                var temp = data[pos];
                data[pos] = item;
                item = temp;
                while (pos != cycleStart)
                {
                    pos = cycleStart;
                    for (var i = cycleStart + 1; i <= data.Length - 1; i++)
                    {
                        if (data[i] < item)
                        {
                            pos++;
                        }
                    }

                    while (data[pos] == item)
                    {
                        pos++;
                    }

                    temp = data[pos];
                    data[pos] = item;
                    item = temp;
                }
            }
        }
    }
}
