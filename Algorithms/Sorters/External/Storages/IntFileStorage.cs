using System.IO;

namespace Algorithms.Sorters.External.Storages
{
    public class IntFileStorage : ISequentialStorage<int>
    {
        private readonly string filename;

        public IntFileStorage(string filename, int length)
        {
            Length = length;
            this.filename = filename;
        }

        public int Length { get; }

        public ISequentialStorageReader<int> GetReader() => new FileReader(filename);

        public ISequentialStorageWriter<int> GetWriter() => new FileWriter(filename);

        private class FileReader : ISequentialStorageReader<int>
        {
            private readonly BinaryReader reader;

            public FileReader(string filename) => reader = new BinaryReader(File.OpenRead(filename));

            public void Dispose() => reader.Dispose();

            public int Read() => reader.ReadInt32();
        }

        private class FileWriter : ISequentialStorageWriter<int>
        {
            private readonly BinaryWriter writer;

            public FileWriter(string filename) => writer = new BinaryWriter(File.OpenWrite(filename));

            public void Write(int value) => writer.Write(value);

            public void Dispose() => writer.Dispose();
        }
    }
}
