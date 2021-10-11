using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Account
{
    public interface IUpdateAccountViewModel
    {
        string AccessCode { get; set; }
        string Error { get; set; }
        string Password { get; set; }
        string PasswordConfirm { get; set; }
        string RoleName { get; set; }
        IEnumerable<string> Roles { get; set; }
        string UserName { get; set; }
        string Id { get; set; }

        Task OnInitialize(string Id);
        Task OnSubmit();
    }
}