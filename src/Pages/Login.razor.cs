using Microsoft.AspNetCore.Components;
using PSI_FRONT.Models;
using PSI_FRONT.Services;
using System.Threading.Tasks;

namespace PSI_FRONT.Pages
{
    public partial class Login
    {
        [Inject]
        IUserService UserService { get; set; }

        private bool OperationResult { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        private User user;

        protected async override Task OnInitializedAsync()
        {
            user = new User();
            OperationResult = true;
        }

        private async Task ValidateUser()
        {
            OperationResult = await UserService.GetLoginAuthenticationAsync(user);

            if (OperationResult)
            {
                Navigation.NavigateTo("/");
            }
        }

    }
}
