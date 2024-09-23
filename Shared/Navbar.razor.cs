using Microsoft.AspNetCore.Components;
using ParagonID.InternalSystem.Helpers;
using ParagonID.InternalSystem.Models;

namespace ParagonID.InternalSystem.Shared
{
    public class NavbarComponent : ComponentBase
    {
        // [Injections]
        [Inject] public NavlinksHelper NavlinksHelper { get; set; }
        //

        // [Properties]
        public IList<Navlink> Navlinks { get; set; }
        //

        // [Fields]
        //

        protected override async Task OnInitializedAsync()
        {
            Navlinks = await NavlinksHelper.GetNavLinks();
            await base.OnInitializedAsync();
        }
    }
}
