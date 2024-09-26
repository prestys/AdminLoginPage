using Microsoft.AspNetCore.Components.Routing;

namespace ParagonID.InternalSystem.Models
{
    public class Navlink
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
    }

    public class NavLinksContainer
    {
        public List<Navlink> Navlinks { get; set; }
    }
}
