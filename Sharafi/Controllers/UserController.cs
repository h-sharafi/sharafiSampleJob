using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.User;
using Application.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sharafi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUSerService _userService;
        public UserController(IUSerService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(Login login)
        {
            return await _userService.Login(login.Email, login.Password);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(Register register)
        {
            return await _userService.Register(register.Email, register.Password);
        }

        [HttpGet]
        public async Task<ActionResult<User>> CurrentUser()
        {
            return await _userService.CurrentUser();
        }
    }
}