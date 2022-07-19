MemoryAlloc();
Console.WriteLine("Hello, World!");

int[] myIntArray = new int[5] { 1, 2, 3, 4, 5 };


static unsafe void MemoryAlloc()
{
    byte* buffer = null;
    try
    {
        const int Size = 256;
        buffer = (byte*)Memory.Alloc(Size);//分配256字节的内存
        for (int i = 0; i < Size; i++)
        {
            buffer[i] = (byte)i;//初始化值从0增加到255的内存块
        }
        byte[] array = new byte[Size];//分配一个256元素字节数组
        fixed (byte* p = array)
        {
            Memory.Copy(buffer, p, Size);//使用将 Memory.Copy 内存块的内容复制到字节数组中
        }
        for (int i = 0; i < Size; i++)
        {
            fixed (byte* ptr = &array[i])
            {
                Console.Write((int)ptr);
                Console.WriteLine($"{*(ulong*)ptr}\t{*ptr}");
            }
        }
    }
    finally
    {
        if (buffer != null)
        {
            Memory.Free(buffer);//释放内存块
        }
    }
}
Console.ReadLine();
