using Microsoft.AspNetCore.Components;
using ParagonID.Data.Models;
using ParagonID.Data.Service;

namespace ParagonID.InternalSystem.Pages.Presses;

public class DashboardModel : ComponentBase
{
    // [Injections]
    [Inject] public DataRetriever DataRetriever { get; set; }
    //

    protected override Task OnInitializedAsync()
    {
        var __allPresses = DataRetriever.GetAll<MaintenanceDB_PressSpec>();
        return base.OnInitializedAsync();
    }
}
