namespace Awesome.BasicTips
{
    using System;
    using System.Text;

    public static class HelloString
    {
        /// <summary>
        /// IntToString(-999)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static string IntToString(int value)
        {
            int n = value >= 0 ? value : -value;
            unsafe
            {
                char* buffer = stackalloc char[16];//在堆栈上分配16个字符的缓冲区,当方法返回时，缓冲区会自动丢弃
                char* p = buffer + 16;
                do
                {
                    *--p = (char)(n % 10 + '0');
                    n /= 10;
                } while (n != 0);
                if (value < 0)
                {
                    *--p = '-';
                }
                return new string(p, 0, (int)(buffer + 16 - p));
            }
        }
        /// <summary>
        /// String must be exactly 8 (ASCII) characters long
        /// fixed 固定对象可能会导致堆 (碎片，因为它们不能) 移动。 因此，仅当绝对必要时才应修复对象，而只应在尽可能最短的时间进行修复
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static unsafe ulong GetAsciiStringAsLong(string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            //fixed用于修复数组，以便可以将其地址传递给采用指针的方法
            fixed (byte* ptr = &bytes[0])
            {
                return *(ulong*)ptr;
            }
        }
    }
}
