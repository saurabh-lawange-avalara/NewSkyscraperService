using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Avalara.Skyscraper.Common
{
    public class CryptoHelper
    {
        //16 char IV should give 32 bit byte array, which is equal to keysize/8.
        private const string initVector = "hsku27ty2176nacl";

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        /// <summary>
        /// Encrypts the string
        /// </summary>
        /// <param name="plainText">string to be encrypted</param>
        /// <param name="passPhrase">password</param>
        /// <returns>encrypted string</returns>
        public static string Encrypt(string plainText, string passPhrase)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                password = null;
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();
                plainText = null;
                plainTextBytes = null;
                passPhrase = null;
                //memoryStream.Close();
                //cryptoStream.Close();
                cryptoStream.Dispose();
                return Convert.ToBase64String(cipherTextBytes);
            }
            else return null;
        }

        /// <summary>
        /// Decrypts the encrypted string
        /// </summary>
        /// <param name="cipherText">encrypted string</param>
        /// <param name="passPhrase">password</param>
        /// <returns>decrypted plaintext</returns>
        public static string Decrypt(string cipherText, string passPhrase)
        {
            if (!string.IsNullOrEmpty(cipherText))
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                password = null;
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                cipherText = null;
                passPhrase = null;
                //memoryStream.Close();
                //cryptoStream.Close();
                cryptoStream.Dispose();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            else return null;
        }
    }
}
