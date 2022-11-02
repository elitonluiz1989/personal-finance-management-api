using System.Security.Cryptography;
using System.Text;

namespace PersonalFinanceManagement.Domain.Base.Tools.Security
{
    public static class Hash
    {
        public static string Encrypt(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return content;

            var encodedContent = Encoding.Default.GetBytes(content);
            var sha = SHA256.Create();
            var hashedContent = sha.ComputeHash(encodedContent);

            return Convert.ToBase64String(hashedContent);
        }
    }
}
