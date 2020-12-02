using Microsoft.AspNetCore.Components;
using PSI_FRONT.Models;
using PSI_FRONT.Services;
using System.Threading.Tasks;

namespace PSI_FRONT.Pages.Auth
{
    public partial class Login
    {
        [Inject]
        IUserService UserService { get; set; }

        private bool OperationResult { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        private User user;

        protected override void OnInitialized()
        {
            user = new User();
            OperationResult = true;
        }

        private async Task ValidateUser()
        {
            OperationResult = await UserService.Login(user);

            if (OperationResult)
            {
                Navigation.NavigateTo("/");
            }
        }

    }
}
