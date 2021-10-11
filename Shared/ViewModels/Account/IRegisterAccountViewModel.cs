using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Account
{
    public interface IRegisterAccountViewModel
    {
        string Password { get; set; }
        string PasswordConfirm { get; set; }
        string RoleName { get; set; }
        IEnumerable<string> Roles { get; set; }
        string UserName { get; set; }
        string Error { get; set; }
        string AccessCode { get; set; }

        Task OnInitialize();
        Task OnSubmit();
    }
}