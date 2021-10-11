using Identity.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Account
{
    public class LoginViewModel : ILoginViewModel
    {
        private readonly IdentityStateProvider _identityStateProvider;
        private readonly NavigationManager _navigationManager;

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string AccessCode { get; set; }
        public string Error { get; set; }

        public LoginViewModel()
        {

        }

        public LoginViewModel(IdentityStateProvider identityStateProvider, NavigationManager navigationManager) : this()
        {
            _identityStateProvider = identityStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task OnSubmit()
        {
            Error = null;
            try
            {
                await _identityStateProvider.Login(this);
                _navigationManager.NavigateTo("");
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }
    }
}
