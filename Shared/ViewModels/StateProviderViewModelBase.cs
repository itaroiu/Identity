using Identity.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Identity.Shared.ViewModels
{
    public class StateProviderViewModelBase
    {
        protected readonly IdentityStateProvider _identityStateProvider;
        protected readonly NavigationManager _navigationManager;

        public StateProviderViewModelBase(IdentityStateProvider identityStateProvider, NavigationManager navigationManager)
        {
            _identityStateProvider = identityStateProvider;
            _navigationManager = navigationManager;
        }
    }
}