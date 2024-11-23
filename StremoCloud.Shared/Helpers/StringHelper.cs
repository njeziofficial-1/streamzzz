using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace StremoCloud.Shared.Helpers;

    public class StringHelper
    {
        public static bool IsValidEmail(string email)
        {
            EmailAddressAttribute emailValidator = new();    
            return emailValidator.IsValid(email);
        }
        public static string GenerateRandomString(int stringLength)
        {
            if (stringLength <= 0)
                throw new ArgumentException("String length must be greater than zero.");
            
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            var result = new StringBuilder(stringLength);
            var byteBuffer = new byte[4];

            using (var rng = RandomNumberGenerator.Create())
            {
                for (var i = 0; i < stringLength; i++)
                {
                    rng.GetBytes(byteBuffer);
                    var num = BitConverter.ToInt32(byteBuffer, 0) & int.MaxValue;
                    result.Append(chars[num % chars.Length]);
                }
            }

            return result.ToString();
        }
    }

