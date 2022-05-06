using System.Security;

namespace Security.Service
{
    public interface ISecurityService
    {
        byte[] EncodeString(SecureString secureString);

        SecureString DecodeString(byte[] chipher);
    }
}