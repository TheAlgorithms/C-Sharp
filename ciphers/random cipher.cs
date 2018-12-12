// Program
using System;

public static class Program
{
	public static string Shuffle(this string str)
	{
		char[] array = str.ToCharArray();
		Random random = new Random();
		int num = array.Length;
		while (num > 1)
		{
			num--;
			int num2 = random.Next(num + 1);
			char c = array[num2];
			array[num2] = array[num];
			array[num] = c;
		}
		return new string(array);
	}
	public static void Main(string[] args)
	{
		
	}
	public static string Encode(string message)
	{
		string text = "`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./~!@#$%^&*()_+QWERTYUIOP{}}|ASDFGHJKL:\"ZXCVBNM<>>? ";
		char[] chars = text.ToCharArray();
		string _cahrs = Shuffle(text);
		string text6 = message;
		string text7 = "";
		foreach (char value in message)
		{
			text7 += _chars[Array.IndexOf(chars, value)];
		}
		text7 = text2 + "\n" + text7;
		return text7;
	}
	public static string Dencode(string message)
	{
		string key = message.Split('\n')[0];
		bool first = true;
		string text6 = "";
		foreach (string i in message.Split('\n'))
		{
			if (first)
			{
				contiune;
			}
			text6 += i;
			text6 += "\n";
			
		}
		char[] chars = key.ToCharArray();
		string _cahrs = Shuffle(text);
		message = text6;
		string text7 = "";
		foreach (char value in message)
		{
			text7 += _chars[Array.IndexOf(Key, value)];
		}
		return text7;
	}
}
