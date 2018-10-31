using System;
using System.Security.Cryptography;
using System.Text;

namespace ReadingList.Application.Services.Encryption
{
    public class DefaultEncryptionAlgorithm : IEncryptionAlgorithm
    {
        public string Execute(string normalString)
        {
            if(string.IsNullOrEmpty(normalString))
                throw new ArgumentNullException(nameof(normalString));
            
            const string localeParameter = "ytrewQ";
            SHA1 sh1 = new SHA1CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(normalString.Insert(normalString.Length - 2, localeParameter));
            var hash = sh1.ComputeHash(bytes);

            var stringBuilder = new StringBuilder();
            foreach (var t in hash)
            {
                stringBuilder.Append(t.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}