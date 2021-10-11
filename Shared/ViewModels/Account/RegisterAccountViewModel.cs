using Identity.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Account
{
    public class RegisterAccountViewModel : StateProviderViewModelBase, IRegisterAccountViewModel
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string PasswordConfirm { get; set; }
        [Required]
        public string RoleName { get; set; }
        [MaxLength(15)]
        public string AccessCode { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();

        public RegisterAccountViewModel(IdentityStateProvider identityStateProvider, NavigationManager navigationManager) : base(identityStateProvider, navigationManager)
        {
        }

        public string Error { get; set; }

        public async Task OnSubmit()
        {
            Error = null;
            try
            {
                await _identityStateProvider.Register(this);
                _navigationManager.NavigateTo("/Accounts");
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        public async Task OnInitialize()
        {
            Roles = await _identityStateProvider.GetRolesAsync();
        }
    }
}
