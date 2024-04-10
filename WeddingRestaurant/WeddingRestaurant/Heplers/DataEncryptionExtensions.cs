using System.Security.Cryptography;
using System.Text;

namespace WeddingRestaurant.Heplers
{
    public static class DataEncryptionExtensions
    {
        public static string ToMd5Hash(this string password)
        {
            using (var md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(password)));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
