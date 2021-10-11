using Identity.Shared.Dtos;
using Identity.Shared.Services;
using Identity.Shared.ViewModels.Account;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Shared.Models
{
    public class IdentityStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _api;
        private CurrentUser _currentUser;

        public IdentityStateProvider(IAuthService api)
        {
            _api = api;
        }

        public async Task<IEnumerable<string>> GetRolesAsync()
        {
            var roles = await _api.GetRoles();

            return roles;
        }

        public async Task<IEnumerable<AccountViewModel>> GetAccountsAsync()
        {
            var accounts = await _api.GetAcconts();

            return accounts;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetCurrentUser();
                if (userInfo.IsAuthenticated)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, _currentUser.UserName) }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        public async Task<CurrentUser> GetCurrentUser()
        {
            if (_currentUser != null && _currentUser.IsAuthenticated)
                return _currentUser;
            _currentUser = await _api.CurrentUserInfo();
            return _currentUser;
        }

        public async Task Logout()
        {
            await _api.Logout();
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Login(LoginViewModel loginParameters)
        {
            await _api.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Register(RegisterAccountViewModel registerParameters)
        {
            await _api.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<UpdateAccountDto> GetUpdateAccount(string accountId)
        {
            return await _api.GetUpdateAccount(accountId);
        }

        public async Task UpdateAccount(UpdateAccountDto updateAccountDto)
        {
            await _api.UpdateAccount(updateAccountDto);
        }
    }
}
