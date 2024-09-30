using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ParagonID.InternalSystem.Helpers;
using ParagonID.InternalSystem.Shared;

namespace ParagonID.InternalSystem.Pages.Presses;

public class DailyModel : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }

    [Parameter]
    public string Press { get; set; }

    public Type Layout { get; set; } = typeof(BlankLayout);

    protected override async Task OnInitializedAsync()
    {
        var __uri = new Uri(NavigationManager.Uri);
        var __query = System.Web.HttpUtility.ParseQueryString(__uri.Query);

        if (bool.TryParse(__query.Get("admin"), out bool isAdmin) && isAdmin)
        {
            Layout = typeof(MainLayout);
        }

        await base.OnInitializedAsync();
    }
}