using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTO;
using BusinessLayer.Helpers;
using BusinessLayer.Interfaces;
using DataAccess.Entities;

namespace BusinessLayer.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _context;
        private readonly ITokenService _tokenService;

        public UserManagementService(IUserRepository context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<UserDto> CreateUser(RegisterDto registerInfo)
        {
            if (await _context.UserExist(registerInfo.UserName))
                return null;

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerInfo.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerInfo.Password)),
                PasswordSalt = hmac.Key
            };
            await _context.CreateUser(user);
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<UserDto> LoginUser(LoginDto loginInfo)
        {
            var userData = new Login
            {
                UserName = loginInfo.UserName,
                Password = loginInfo.Password
            };

            var user = await _context.LoginUser(userData);
            if (user == null)
                return null;

            return !Helper.ComparePasswords(user, loginInfo) ? null : new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            }; ;
        }
    }
}
