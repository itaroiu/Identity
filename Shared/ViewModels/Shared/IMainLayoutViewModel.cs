using System.Threading.Tasks;

namespace Identity.Shared.ViewModels.Shared
{
    public interface IMainLayoutViewModel
    {
        void Login();
        Task RedirectIfNotAuthenticatedAsync();
    }
}