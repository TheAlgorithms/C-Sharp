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

	public static string T2H(string asciiString)
	{
		string text = "";
		foreach (char c in asciiString)
		{
			int num = c;
			text += $"{Convert.ToUInt32(num.ToString()):x2}";
		}
		return text;
	}

	public static string H2T(string HexValue)
	{
		string text = "";
		while (HexValue.Length > 0)
		{
			text += Convert.ToChar(Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
			HexValue = HexValue.Substring(2, HexValue.Length - 2);
		}
		return text;
	}

	public static string H2O(string hex)
	{
		hex = hex.ToLower();
		hex.Replace("8", "10");
		hex.Replace("9", "11");
		hex.Replace("a", "12");
		hex.Replace("b", "13");
		hex.Replace("c", "14");
		hex.Replace("d", "15");
		hex.Replace("e", "16");
		hex.Replace("f", "17");
		return hex;
	}

	public static string O2H(string hex)
	{
		return Convert.ToInt64(hex, 16).ToString();
	}

	public static void Main(string[] args)
	{
		string text = "`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./~!@#$%^&*()_+QWERTYUIOP{}}|ASDFGHJKL:\"ZXCVBNM<>>? ";
		char[] array = text.ToCharArray();
		string text2 = Shuffle(text);
		string text3 = Shuffle(text);
		string text4 = Shuffle(text);
		string text5 = Shuffle(text);
		Console.WriteLine("Enter A message:");
		string text6 = Console.ReadLine();
		string text7 = "";
		string text8 = "";
		string text9 = "";
		Console.WriteLine(text6 + "\n\n");
		string text10 = text6;
		foreach (char value in text10)
		{
			text7 += text2[Array.IndexOf(array, value)];
		}
		text7 = text2 + "\n" + text7;
		Console.WriteLine(text7);
		text7 = T2H(text7);
		text10 = text7;
		foreach (char value in text10)
		{
			text8 += text3[Array.IndexOf(array, value)];
		}
		text8 = text3 + "\n" + text8;
		text8 = H2O(T2H(text8));
		text10 = text8;
		foreach (char value in text10)
		{
			text9 += text4[Array.IndexOf(array, value)];
		}
		text9 = T2H(text7);
		Console.WriteLine(text9);
		Console.Read();
	}
}
