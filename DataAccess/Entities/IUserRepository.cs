using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsers();
        Task<AppUser> GetUser(int id);
        Task<AppUser> CreateUser(AppUser user);
        Task<bool> UserExist(string username);
        Task<AppUser> LoginUser(Login userData);
    }
}
