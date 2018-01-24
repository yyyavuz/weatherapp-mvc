using System.Security.Cryptography;
using System.Text;

namespace Kontrolmatik
{
    public static class StringExtensions
    {
        public static string Hash(this string text)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = System.String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(text), 0, Encoding.ASCII.GetByteCount(text));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}