using Identity.Shared.Dtos;
using Identity.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Account
{
    public class UpdateAccountViewModel : StateProviderViewModelBase, IUpdateAccountViewModel
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string PasswordConfirm { get; set; }
        [Required]
        public string RoleName { get; set; }
        [MaxLength(15)]
        public string AccessCode { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();

        public UpdateAccountViewModel(IdentityStateProvider identityStateProvider, NavigationManager navigationManager) : base(identityStateProvider, navigationManager)
        {
        }

        public string Error { get; set; }

        public async Task OnSubmit()
        {
            Error = null;
            try
            {
                await _identityStateProvider.UpdateAccount((UpdateAccountDto)this);
                _navigationManager.NavigateTo("/Accounts");
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        public async Task OnInitialize(string accountId)
        {
            Id = accountId;
            var updateAccount = await _identityStateProvider.GetUpdateAccount(accountId);

            UserName = updateAccount.UserName;
            Roles = updateAccount.Roles;
            RoleName = updateAccount.RoleName;
            AccessCode = updateAccount.AccessCode;
        }
    }
}
