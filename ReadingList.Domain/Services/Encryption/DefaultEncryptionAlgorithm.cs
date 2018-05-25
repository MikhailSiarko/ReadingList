using System;
using System.Security.Cryptography;
using System.Text;

namespace ReadingList.Domain.Services.Encryption
{
    public class DefaultEncryptionAlgorithm : IEncryptionAlgorithm
    {
        private readonly Func<string, string> _algorithm;

        public DefaultEncryptionAlgorithm()
        {
            _algorithm = normalString =>
            {
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
            };
        }

        public string Execute(string normalString) => _algorithm(normalString);
    }
}