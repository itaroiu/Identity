using Identity.Shared.Dtos;
using Identity.Shared.Models;
using Identity.Shared.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Identity.Shared.Services
{
    public class AccountService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CurrentUser> CurrentUserInfo()
        {
            var result = await _httpClient.GetFromJsonAsync<CurrentUser>("api/Account/CurrentUserInfo");
            return result;
        }

        public async Task Login(LoginViewModel loginViewModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Account/Login", loginViewModel);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/Account/Logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterAccountViewModel registerViewModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Account/Register", (RegisterAccountDto)registerViewModel);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<string>> GetRoles()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<string>>("api/Account/GetAllRoles");
            return result;
        }

        public async Task<IEnumerable<AccountViewModel>> GetAcconts()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<AccountDto>>("api/Account/GetAllAccounts");
            return result.Select(a => (AccountViewModel)a);
        }

        public async Task<UpdateAccountDto> GetUpdateAccount(string id)
        {
            var result = await _httpClient.PostAsJsonAsync<string>("api/Account/GetUpdateAccount", id);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            return await result.Content.ReadFromJsonAsync<UpdateAccountDto>();
        }

        public async Task UpdateAccount(UpdateAccountDto  updateAccountDto)
        {
            var result = await _httpClient.PostAsJsonAsync<UpdateAccountDto>("api/Account/UpdateAccount", updateAccountDto);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }
    }
}
