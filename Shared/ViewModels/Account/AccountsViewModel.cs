using Identity.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Account
{
    public class AccountsViewModel : StateProviderViewModelBase, IAccountsViewModel
    {
        public List<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();

        public AccountsViewModel(IdentityStateProvider identityStateProvider, NavigationManager navigationManager) : base(identityStateProvider, navigationManager)
        {
        }

        public async Task OnInitialize()
        {
            var accounts = await _identityStateProvider.GetAccountsAsync();

            Accounts = accounts.ToList();
        }

        public void UpdateAccount(string accountId)
        {
            _navigationManager.NavigateTo($"Account/Update/{accountId}");
        }
    }
}
