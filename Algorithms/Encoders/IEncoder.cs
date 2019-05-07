/// <summary>
/// Summary description for Class1
/// </summary>
public interface IEncoder
{
    string Encode(string value, string key);
    string Decode(string value, string key);
}
