using Identity.Server.Models;
using Identity.Shared.Dtos;
using Identity.Shared.Models;
using Identity.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto request)
        {
            ApplicationUser user;
            Microsoft.AspNetCore.Identity.SignInResult singInResult;

            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            {
                user = _userManager.Users.SingleOrDefault(u => u.AccessCode == request.AccessCode);
                if (user == null)
                {
                    return BadRequest("Invalid access code.");
                }
                await _signInManager.SignInWithClaimsAsync(user, request.RememberMe, new[] { new Claim(ClaimTypes.AuthenticationMethod, "AccessCode") });
            }
            else
            {
                user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                    return BadRequest("User does not exist.");
                singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!singInResult.Succeeded)
                    return BadRequest("Invalid password.");
                await _signInManager.SignInWithClaimsAsync(user, request.RememberMe, new[] { new Claim(ClaimTypes.AuthenticationMethod, "UserAndPassword") });
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccountDto registerDto)
        {
            var user = (ApplicationUser)registerDto;

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors.FirstOrDefault()?.Description);

            await _userManager.AddToRoleAsync(user, registerDto.RoleName);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        public CurrentUser CurrentUserInfo()
        {
            return new CurrentUser
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                Claims = User.Claims
                .ToDictionary(c => c.Type, c => c.Value)
            };
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<string> GetAllRoles()
        {
            var roles = _roleManager.Roles.OrderBy(r => r.Name)
                  .Select(r => r.Name)
                  .ToList();

            return roles;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<AccountDto>> GetAllAccounts()
        {
            List<AccountDto> usersDto = new List<AccountDto>();

            foreach (var role in _roleManager.Roles.ToList())
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                usersDto.AddRange(usersInRole.Select(a => new AccountDto
                {
                    UserId = a.Id,
                    UserName = a.UserName,
                    RoleName = role.Name
                }));
            }

            return usersDto.OrderBy(u => u.UserName);
        }

        [Authorize]
        [HttpPost]
        public async Task<UpdateAccountDto> GetUpdateAccount([FromBody] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var updateAccountDto = new UpdateAccountDto
            {
                Id = user.Id,
                AccessCode = user.AccessCode,
                UserName = user.UserName,
                RoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
                Roles = _roleManager.Roles.OrderBy(r => r.Name).Select(r => r.Name).ToList()
            };
            return updateAccountDto;
        }

        [Authorize]
        [HttpPost]
        public async Task UpdateAccount([FromBody] UpdateAccountDto accountDto)
        {
            var user = await _userManager.FindByIdAsync(accountDto.Id);

            user.UserName = accountDto.UserName;
            user.AccessCode = accountDto.AccessCode;

            await _userManager.UpdateAsync(user);

            if (!string.IsNullOrWhiteSpace(accountDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var changePasswordResult = await _userManager.ResetPasswordAsync(user, token, accountDto.Password);
                if (!changePasswordResult.Succeeded)
                {
                    throw new Exception("Error while updating password.");
                }
            }

            if (!await _userManager.IsInRoleAsync(user, accountDto.RoleName))
            {
                var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                if (currentRole != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, currentRole);
                }

                await _userManager.AddToRoleAsync(user, accountDto.RoleName);
            }
        }
    }
}
