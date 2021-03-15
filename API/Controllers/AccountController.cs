using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTO;
using BusinessLayer.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserRepository _context;
        private readonly IUserManagementService _userManagementService;

        public AccountController(IUserRepository context, IUserManagementService userManagementService)
        {
            _context = context;
            _userManagementService = userManagementService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerData)
        {
            var user = await _userManagementService.CreateUser(registerData);
            if (user == null)
            {
                return BadRequest("This username is already taken");
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginData)
        {
            var user = await _userManagementService.LoginUser(loginData);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }
    }
}
