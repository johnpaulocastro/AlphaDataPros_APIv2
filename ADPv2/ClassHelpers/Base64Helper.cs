using System.Text;

namespace ADPv2.ClassHelpers
{
    public class Base64Helper
    {
        public static string EncryptToBase64(string dataToEncrypt)
        {
            byte[] jsonBytes = Encoding.UTF8.GetBytes(dataToEncrypt);
            return Convert.ToBase64String(jsonBytes);
        }

        public static string DecryptFromBase64(string dataToDecrypt)
        {
            byte[] jsonBytes = Convert.FromBase64String(dataToDecrypt);
            return Encoding.UTF8.GetString(jsonBytes);
        }
    }
}
