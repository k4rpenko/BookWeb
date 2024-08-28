using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace BDLearn.Hash
{
    public class HASH
    {
        private readonly byte[] Key;
        private readonly byte[] Iv;

        public HASH() 
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            Key = Convert.FromBase64String(builder.GetSection("AES-256:Key").Value);
            Iv = Convert.FromBase64String(builder.GetSection("AES-256:IV").Value);
        }


        public string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
