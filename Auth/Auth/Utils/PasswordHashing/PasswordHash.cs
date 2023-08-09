using System.Security.Cryptography;
using System.Text;

namespace Auth.Utils.PasswordHashing
{
    public class PasswordHash : IPasswordHash
    {
        bool IPasswordHash.ComparePassword(string UserPassword, string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = Convert.FromBase64String(UserPassword);
            byte[] encryptPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            if (!hashedBytes.SequenceEqual(encryptPassword))
            {
                return false;
            }
            return true;
        }

        string IPasswordHash.HashedPassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
