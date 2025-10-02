using System.IO;

namespace Algorithms.Sorters.External.Storages;

public class IntFileStorage(string filename, int length) : ISequentialStorage<int>
{
    private readonly string filename = filename;

    public int Length { get; } = length;

    public ISequentialStorageReader<int> GetReader() => new FileReader(filename);

    public ISequentialStorageWriter<int> GetWriter() => new FileWriter(filename);

    private class FileReader(string filename) : ISequentialStorageReader<int>
    {
        private readonly BinaryReader reader = new BinaryReader(File.OpenRead(filename));

        public void Dispose() => reader.Dispose();

        public int Read() => reader.ReadInt32();
    }

    private class FileWriter(string filename) : ISequentialStorageWriter<int>
    {
        private readonly BinaryWriter writer = new BinaryWriter(File.OpenWrite(filename));

        public void Write(int value) => writer.Write(value);

        public void Dispose() => writer.Dispose();
    }
}
