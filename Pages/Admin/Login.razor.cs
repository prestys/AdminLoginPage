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
        // [Injections]
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] JWTHelper JWTHelper { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        //

        // [Properties]
        public string Email { get; set; }
        public string Password { get; set; }
        //

        // [Fields]
        public bool Authenticated = false;
        //

        /// <summary>
        /// Executes the routine for logging in.
        /// </summary>
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

        /// <summary>
        /// This validates the credentials that are entered.
        /// </summary>
        /// <returns></returns>
        public bool CredentialValidation()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// This will execute the JS code that creates the JWT with the relevant credentials.
        /// </summary>
        private async void CreateJWT()
        {
            string __jwt = JWTHelper.GenerateJwtToken("REPLACE_THIS_WITH_USER_ID");
            string __expiry = DateTime.UtcNow.AddMonths(1).ToString("R");

            await JSRuntime.InvokeVoidAsync("setJwtCookie", __jwt, __expiry);
        }
    }
}
