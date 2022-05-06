using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Security.Service
{
    internal sealed class SecurityService : ISecurityService
    {
        public byte[] EncodeString(SecureString secureString)
        {
            string pass = new NetworkCredential(null, secureString).Password;

            byte[] data = Encoding.UTF8.GetBytes(pass);

            using Aes aes = Aes.Create();
            byte[] key =
            {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
            };
            aes.Key = key;

            byte[] iv = aes.IV;
            byte[] code = aes.EncryptCfb(data, iv);

            List<byte> result = new(iv.Length + code.Length);

            result.AddRange(iv);
            result.AddRange(code);

            return result.ToArray();
        }

        public SecureString DecodeString(byte[] chipher)
        {
            using Aes aes = Aes.Create();
            byte[] key =
            {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
            };
            aes.Key = key;

            List<byte> code = new(chipher);

            int ivLenght = aes.IV.Length;
            byte[] iv = code.GetRange(0, ivLenght).ToArray();
            code.RemoveRange(0, ivLenght);

            byte[] decodedBytes = aes.DecryptCfb(code.ToArray(), iv);
            string decoded = Encoding.UTF8.GetString(decodedBytes);

            SecureString secureString = new();
            foreach (char character in decoded)
                secureString.AppendChar(character);

            secureString.MakeReadOnly();

            return secureString;
        }
    }
}