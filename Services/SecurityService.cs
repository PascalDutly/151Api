using System.Security.Cryptography;

namespace Csbe.Todo.Apil.Services
{
    public static class SecurityService
    {
        // Authenticate method
        public static bool CanAuthenticate(byte[] inputPassword, byte[] storedPassword)
        {
            for(int i = 0; i < inputPassword.Length; i++)
            {
                if(inputPassword[i] != storedPassword[i]) return false;
            }
            return true;
        }

        // Hash method (for pw and salt)
        public static byte[] Hash(byte[] inputToHash)
        {
            byte[] hashedInput;

            using(SHA256 sha = SHA256.Create())
            {
                hashedInput = sha.ComputeHash(inputToHash);
            }
            return hashedInput;
        }

        //Method to genererate pw with hash, salt and pepper TODO

    }
}