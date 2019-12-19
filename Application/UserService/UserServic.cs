using Application.Errors;
using Application.Interfaces;
using Application.User;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserService
{
    public interface IUSerService : IEntityService<AppUser>
    {
        Task<User.User> Login(string Email, string Password);
        Task<User.User> Register(string Email, string Password);
        Task<User.User> CurrentUser();
    }
    public class UserServic : EntityService<AppUser>, IUSerService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserAccessor _userAccessor;

        public UserServic(DataContext context, UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator
            , IUserAccessor userAccessor)
            : base(context)
        {
            
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _userAccessor = userAccessor;
        }

        public async Task<User.User> CurrentUser()
        {
            var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

            return new User.User
            {

                Username = user.UserName,
                Token = _jwtGenerator.CreateToken(user),
            };
        }

        public async Task<User.User> Login(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
                throw new RestException(HttpStatusCode.Unauthorized);

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, Password, false);

            if (result.Succeeded)
            {
                // TODO: generate token
                return new User.User
                {
                    Token = _jwtGenerator.CreateToken(user),
                    Username = user.UserName,
                };
            }

            throw new RestException(HttpStatusCode.Unauthorized);
        }

        public async Task<User.User> Register(string Email, string Password)
        {
            if (await _context.Users.Where(x => x.Email == Email).AnyAsync())
                throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });

            if (await _context.Users.Where(x => x.Email == Email).AnyAsync())
                throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });

            var user = new AppUser
            {
                Email = Email,
                UserName = Email
            };

            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                return new User.User
                {
                    Token = _jwtGenerator.CreateToken(user),
                    Username = user.UserName,
                };
            }

            throw new Exception("Problem creating user");
        }
    }
}
