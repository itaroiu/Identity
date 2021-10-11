using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Account
{
    public interface ILoginViewModel
    {
        string Password { get; set; }
        bool RememberMe { get; set; }
        string UserName { get; set; }
        string Error { get; set; }

        Task OnSubmit();
    }
}