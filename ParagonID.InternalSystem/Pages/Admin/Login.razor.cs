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
        [Inject] public NotificationService NotificationService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public JWTHelper JWTHelper { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AuthorisationHelper AuthorisationHelper { get; set; }
        //

        // [Properties]
        public string Email { get; set; }
        public string Password { get; set; }
        //

        // [Fields]
        public bool Authenticated = false;
        //

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                AccountAuthorisation();
            }

            base.OnAfterRender(firstRender);
        }

        /// <summary>
        /// Handles the navigation if the jwt was to fail.
        /// </summary>
        private async void AccountAuthorisation()
        {
            string __jwt = await JSRuntime.InvokeAsync<string>("getJwtCookie");
            bool __authResult = await AuthorisationHelper.IsAuthorized(__jwt);

            if (__authResult)
            {
                var __uri = new Uri(NavigationManager.Uri);
                string __path = __uri.LocalPath;

                if (__path.Contains("login"))
                {
                    NavigationManager.NavigateTo("/");
                }
            }
            else
            {
                NavigationManager.NavigateTo("/admin/login");
            }
        }

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
