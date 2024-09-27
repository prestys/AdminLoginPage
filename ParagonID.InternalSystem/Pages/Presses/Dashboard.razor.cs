using Microsoft.AspNetCore.Components;
using ParagonID.Data.Models;
using ParagonID.Data.Service;

namespace ParagonID.InternalSystem.Pages.Presses;

public class DashboardModel : ComponentBase
{
    // [Injections]
    [Inject] public DataRetriever DataRetriever { get; set; }
    //

    // [Properties]
    public IEnumerable<MaintenanceDB_PressSpec> Presses = new List<MaintenanceDB_PressSpec>();
    //

    protected override Task OnInitializedAsync()
    {
        Presses = DataRetriever.GetAll<MaintenanceDB_PressSpec>();
        return base.OnInitializedAsync();
    }
}
