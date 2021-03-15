using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTO;
using DataAccess.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IUserManagementService
    {
        Task<UserDto> CreateUser(RegisterDto registerInfo);
        Task<UserDto> LoginUser(LoginDto loginInfo);
    }
}
