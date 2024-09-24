using Microsoft.AspNetCore.Components;
using ParagonID.InternalSystem.Helpers;
using ParagonID.InternalSystem.Models;

namespace ParagonID.InternalSystem.Shared
{
    public class NavbarComponent : ComponentBase
    {
        // [Injections]
        [Inject] public NavlinksHelper NavlinksHelper { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
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

        /// <summary>
        /// Gets the url path to determine the active page.
        /// </summary>
        private void GetActivePage()
        {
            var __uri = new Uri(NavigationManager.Uri);
            ActivePath = __uri.LocalPath;
        }
    }
}
