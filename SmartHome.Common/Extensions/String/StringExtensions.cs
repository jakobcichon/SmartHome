using System.Text;

namespace SmartHome.Common.Extensions.String;

public static class StringExtensions
{
    public static byte[] ToUtf8(this string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }
}