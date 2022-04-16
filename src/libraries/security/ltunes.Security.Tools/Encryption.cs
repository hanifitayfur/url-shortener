using System;
using System.Security.Cryptography;
using System.Text;

namespace ltunes.Security.Tools
{
    public class Encryption
    {
        private static string _KEY = "l0t2t3a4r5d6!";

        public static string Encrypt(string toEncrypt)
        {
            return Encrypt(toEncrypt, true);
        }

        private static string Encrypt(string toEncrypt, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
                if (useHashing)
                {
                    var hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(_KEY));
                }
                else
                {
                    keyArray = Encoding.UTF8.GetBytes(_KEY);
                }

                var tides = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7
                };

                var cTransform = tides.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return toEncrypt;
            }
        }

        public static string Decrypt(string toDecrypt)
        {
            return Decrypt(toDecrypt, true);
        }

        private static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;

            var toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(_KEY));
                hashmd5.Clear();
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(_KEY);
            }

            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock
                (toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }

    }
}