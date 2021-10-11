using Identity.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Shared
{
    public class NavMenuViewModel : StateProviderViewModelBase, INavMenuViewModel
    {
        public NavMenuViewModel(IdentityStateProvider identityStateProvider, NavigationManager navigationManager) : base(identityStateProvider, navigationManager)
        {
        }

        public async Task Logout()
        {
            await _identityStateProvider.Logout();
            _navigationManager.NavigateTo("/");
        }
    }
}
