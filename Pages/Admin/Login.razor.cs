using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using ParagonID.InternalSystem.Helpers;
using Radzen;
using Radzen.Blazor;

namespace ParagonID.InternalSystem.Pages.Admin
{
    public class LoginModel : ComponentBase
    {
        // Injections
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] JWTHelper JWTHelper { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        //

        public string Email { get; set; }
        public string Password { get; set; }

        public bool Authenticated = false;

        public void LoginRoutine()
        {
            bool __credentialValidity = CredentialValidation();

            if (__credentialValidity)
            {
                CreateJWT();
                NavigationManager.NavigateTo("/");
            }

            Authenticated = true;
        }

        public bool CredentialValidation()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            return true;
        }

        private async void CreateJWT()
        {
            string __jwt = JWTHelper.GenerateJwtToken("REPLACE_THIS_WITH_USER_ID");
            string __expiry = DateTime.UtcNow.AddMonths(1).ToString("R");

            await JSRuntime.InvokeVoidAsync("setJwtCookie", __jwt, __expiry);
        }
    }
}
