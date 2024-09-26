using Newtonsoft.Json;
using ParagonID.InternalSystem.Models;

namespace ParagonID.InternalSystem.Helpers
{
    public class NavlinksHelper
    {
        public async Task<List<Navlink>> GetNavLinks()
        {
            var __filePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Json", "navlinks.json");
            var __jsonString = await File.ReadAllTextAsync(__filePath);
            var __navLinksContainer = JsonConvert.DeserializeObject<NavLinksContainer>(__jsonString);
            return __navLinksContainer.Navlinks;
        }
    }
}
