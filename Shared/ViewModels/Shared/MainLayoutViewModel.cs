using Identity.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Shared
{
    public class MainLayoutViewModel : StateProviderViewModelBase, IMainLayoutViewModel
    {
        public MainLayoutViewModel(IdentityStateProvider identityStateProvider, NavigationManager navigationManager) : base(identityStateProvider, navigationManager)
        {
        }

        public void Login()
        {
            _navigationManager.NavigateTo("/login");
        }

        public async Task RedirectIfNotAuthenticatedAsync()
        {
            var currentUser = await _identityStateProvider.GetCurrentUser();
            if (!currentUser.IsAuthenticated)
            {
                _navigationManager.NavigateTo("/login");
            }
        }
    }
}
