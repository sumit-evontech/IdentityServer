namespace Auth.Utils.PasswordHashing
{
    public interface IPasswordHash
    {
        public string HashedPassword(string password);
        public bool ComparePassword(string encryptedPassword, string password);
    }
}
