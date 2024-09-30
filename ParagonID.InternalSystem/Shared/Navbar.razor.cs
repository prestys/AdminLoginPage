using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ParagonID.InternalSystem.Helpers;
using ParagonID.InternalSystem.Models;

namespace ParagonID.InternalSystem.Shared
{
    public class NavbarComponent : ComponentBase
    {
        // [Injections]
        [Inject] public NavlinksHelper NavlinksHelper { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AuthorisationHelper AuthorisationHelper { get; set; }
        //

        // [Properties]
        public IList<Navlink> Navlinks { get; set; }
        //

        // [Fields]
        public string ActivePath = string.Empty;
        //

        protected override async Task OnInitializedAsync()
        {
            Navlinks = await NavlinksHelper.GetNavLinks();
            GetActivePage();
            await base.OnInitializedAsync();
        }

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
                var __uri = new Uri(NavigationManager.Uri);
                string __path = __uri.LocalPath;

                if (!(__path.Contains("presses/daily") || __path.Contains("presses/weekly") || __path.Contains("presses/monthly")))
                {
                    NavigationManager.NavigateTo("/admin/login");
                }
            }
        }

        /// <summary>
        /// Gets the url path to determine the active page.
        /// </summary>
        private void GetActivePage()
        {
            var __uri = new Uri(NavigationManager.Uri);
            ActivePath = __uri.LocalPath;
        }

        /// <summary>
        /// This will logout the user and delet the cookie.
        /// </summary>
        public async void Logout()
        {
            await JSRuntime.InvokeVoidAsync("deleteJwtCookie");
            NavigationManager.NavigateTo("/admin/login");
        }

        public void NavlinkNavigation(string path)
        {
            NavigationManager.NavigateTo($"{path}");
            ActivePath = path;
        }
    }
}
