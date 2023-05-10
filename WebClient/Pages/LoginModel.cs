using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebClient.Infrastructure;

namespace WebClient.Pages
{
    public class LoginModel : ComponentBase
    {
        [Inject] public ILocalStorageService LocalStorageService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }


        public LoginModel()
        {
            LoginData = new LoginViewModel();
        }

        public LoginViewModel LoginData { get; set; }

        protected async Task LoginAsync()
        {
            var token = new SecurityToken
            {
                AccessToken = LoginData.Password,
                UserName = LoginData.UserName,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };
            await LocalStorageService.SetAsync(nameof(SecurityToken), token);

            NavigationManager.NavigateTo("/fetchdata");
        }

    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Too Long")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class SecurityToken
    {
        public string UserName { get; set; }

        public string AccessToken { get; set; }
        
        public DateTime ExpiredAt { get; set; }
    }


}
