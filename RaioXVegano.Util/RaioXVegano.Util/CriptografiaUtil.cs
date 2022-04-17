using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RaioXVegano.Util
{
    public static class CriptografiaUtil
    {
        //private static readonly byte[] AES_KEY = ASCIIEncoding.ASCII.GetBytes("1234567890123456");
        //private static readonly byte[] AES_IV = ASCIIEncoding.ASCII.GetBytes("1234567890123456");

        private static string _aesKey;
        private static string _aesIV;

        public static string AESKey { get { return _aesKey; } }
        public static string AESIV { get { return _aesIV; } }

        public static void GerarChaves()
        {
            using (Aes aes = Aes.Create())
            {
                if (string.IsNullOrEmpty(AESKey))
                {
                    aes.GenerateKey();
                    _aesKey = Convert.ToBase64String(aes.Key);
                }

                if (string.IsNullOrEmpty(AESIV))
                {
                    aes.GenerateIV();
                    _aesIV = Convert.ToBase64String(aes.IV);
                }
            }
        }

        public static void PreencherComChavesExistentes(string aesKey, string aesIV)
        {
            _aesKey = aesKey;
            _aesIV = aesIV;
        }

        public static string Criptografar(string stringDescriptografada)
        {
            string stringCriptografada = string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(AESKey);
                aes.IV = Convert.FromBase64String(AESIV);

                using (ICryptoTransform ct = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(stringDescriptografada);
                        }

                        stringCriptografada = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }

            return stringCriptografada;
        }

        public static string Descriptografar(string stringCriptografada)
        {
            string stringDescriptografada = string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(AESKey);
                aes.IV = Convert.FromBase64String(AESIV);

                using (ICryptoTransform ct = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(stringCriptografada)))
                    {
                        CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            stringDescriptografada = sr.ReadToEnd();
                        }
                    }
                }
            }

            return stringDescriptografada;
        }
    }
}
