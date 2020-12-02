using Microsoft.AspNetCore.Components;
using PSI_FRONT.Models;
using PSI_FRONT.Services;
using System.Threading.Tasks;

namespace PSI_FRONT.Pages.Auth
{
    public partial class Cadastro
    {
        private User user { get; set; }
        private bool OperationResult { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        [Inject]
        private IUserService UserService { get; set; }

        protected async override void OnInitialized()
        {
            user = new User();
            OperationResult = true;
        }

        private async Task AddUserAsync()
{
            OperationResult = await UserService.AddUserAsync(user);

            if (OperationResult)
            {
                Navigation.NavigateTo("/Login");
            }
        }
    }
}
