using Microsoft.AspNetCore.Components;

namespace ParagonID.InternalSystem.Pages.Presses;

public class DailyModel : ComponentBase
{
    [Parameter]
    public string Press { get; set; }
}