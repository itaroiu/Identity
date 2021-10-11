using Identity.Shared.Dtos;
using Identity.Shared.Models;
using Identity.Shared.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Shared.Services
{
    public interface IAuthService
    {
        Task Login(LoginViewModel loginRequest);
        Task Register(RegisterAccountViewModel registerRequest);
        Task Logout();
        Task<CurrentUser> CurrentUserInfo();
        Task<IEnumerable<string>> GetRoles();
        Task<IEnumerable<AccountViewModel>> GetAcconts();
        Task<UpdateAccountDto> GetUpdateAccount(string id);
        Task UpdateAccount(UpdateAccountDto updateAccountDto);
    }
}
