namespace heap_sort
{
    internal class Program
    {
        private static void Sort(int[] data)
        {
            var heapSize = data.Length;
            for (var p = (heapSize - 1) / 2; p >= 0; p--)
            {
                MakeHeap(data, heapSize, p);
            }

            for (var i = data.Length - 1; i > 0; i--)
            {
                var temp = data[i];
                data[i] = data[0];
                data[0] = temp;

                heapSize--;
                MakeHeap(data, heapSize, 0);
            }
        }

        private static void MakeHeap(int[] input, int heapSize, int index)
        {
            var left = (index + 1) * 2 - 1;
            var right = (index + 1) * 2;
            var largest = left < heapSize && input[left] > input[index] ? left : index;

            // finds the index of the largest
            if (right < heapSize && input[right] > input[largest])
            {
                largest = right;
            }

            if (largest != index)
            {
                // process of reheaping / swapping
                var temp = input[index];
                input[index] = input[largest];
                input[largest] = temp;

                MakeHeap(input, heapSize, largest);
            }
        }
    }
}
