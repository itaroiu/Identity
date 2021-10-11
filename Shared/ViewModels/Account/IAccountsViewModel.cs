using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Account
{
    public interface IAccountsViewModel
    {
        List<AccountViewModel> Accounts { get; set; }

        Task OnInitialize();
        void UpdateAccount(string accountId);
    }
}