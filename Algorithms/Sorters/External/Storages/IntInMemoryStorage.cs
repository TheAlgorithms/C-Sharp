namespace Algorithms.Sorters.External.Storages;

public class IntInMemoryStorage : ISequentialStorage<int>
{
    private readonly int[] storage;

    public IntInMemoryStorage(int[] array) => storage = array;

    public int Length => storage.Length;

    public ISequentialStorageReader<int> GetReader() => new InMemoryReader(storage);

    public ISequentialStorageWriter<int> GetWriter() => new InMemoryWriter(storage);

    private class InMemoryReader : ISequentialStorageReader<int>
    {
        private readonly int[] storage;
        private int offset;

        public InMemoryReader(int[] storage) => this.storage = storage;

        public void Dispose()
        {
            // Nothing to dispose here
        }

        public int Read() => storage[offset++];
    }

    private class InMemoryWriter : ISequentialStorageWriter<int>
    {
        private readonly int[] storage;
        private int offset;

        public InMemoryWriter(int[] storage) => this.storage = storage;

        public void Write(int value) => storage[offset++] = value;

        public void Dispose()
        {
            // Nothing to dispose here
        }
    }
}
