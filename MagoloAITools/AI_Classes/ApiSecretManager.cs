
using System.Security.Cryptography;

namespace MagoloAITools.AI_Classes
{
    public static class ApiSecretManager
    {
        // Nota: Para uso en producción, deberías almacenar estas claves de forma segura, no en el código
        // Estas claves deberían ser generadas aleatoriamente y guardadas de forma segura
        private static readonly byte[] Key = new byte[32] { 
            // Reemplaza esto con tu propia clave de 32 bytes
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16,
            0x17, 0x18, 0x19, 0x20, 0x21, 0x22, 0x23, 0x24,
            0x25, 0x26, 0x27, 0x28, 0x29, 0x30, 0x31, 0x32
        };

        private static readonly byte[] IV = new byte[16] {
            // Reemplaza esto con tu propio IV de 16 bytes
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };

        public static string EncryptSecret(string apiSecret)
        {
            if (string.IsNullOrEmpty(apiSecret))
                return string.Empty;

            byte[] encrypted;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(apiSecret);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptSecret(string encryptedSecret)
        {
            if (string.IsNullOrEmpty(encryptedSecret))
                return string.Empty;

            string plainText;
            byte[] cipherText = Convert.FromBase64String(encryptedSecret);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            plainText = sr.ReadToEnd();
                        }
                    }
                }
            }

            return plainText;
        }

        // Método para generar claves aleatorias (úsalo solo una vez para generar las claves)
        public static void GenerateRandomKeys()
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                Console.WriteLine("Generated Key (copy to your code):");
                Console.WriteLine(BitConverter.ToString(aes.Key).Replace("-", ", 0x"));

                Console.WriteLine("Generated IV (copy to your code):");
                Console.WriteLine(BitConverter.ToString(aes.IV).Replace("-", ", 0x"));
            }
        }
    }
}
