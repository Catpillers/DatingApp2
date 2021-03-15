using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTO;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Helpers
{
    public static class Helper
    {
        public static bool ComparePasswords(AppUser user, LoginDto userLoginInfo)
        {
           using var hmac = new HMACSHA512(user.PasswordSalt);
           var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLoginInfo.Password));

           for (int i = 0; i < computedHash.Length; i++)
           {
               if (computedHash[i] != user.PasswordHash[i])
               {
                   return false;
               }
           }

           return true;
        }
    }
}
