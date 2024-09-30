using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ParagonID.Data.Models;
using ParagonID.Data.Service;
using ParagonID.InternalSystem.Helpers;
using ParagonID.InternalSystem.Shared;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ParagonID.InternalSystem.Pages.Presses;

public class DailyModel : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public DataRetriever DataRetriever { get; set; }

    [Parameter]
    public string Press { get; set; }

    public Type Layout { get; set; } = typeof(BlankLayout);
    public MaintenanceDB_Daily DailyCheck = new MaintenanceDB_Daily();

    public bool IsComplete = false;

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

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            var __uri = new Uri(NavigationManager.Uri);
            var __query = System.Web.HttpUtility.ParseQueryString(__uri.Query);

            string __date = __query?.Get("date")!;

            if (!string.IsNullOrEmpty(__date))
            {
                RetrieveCheck(Press, __date);
                IsComplete = true;
                StateHasChanged();
            }
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private void RetrieveCheck(string press, string date)
    {
        string __pressPattern = @"^p\d{1,2}$";
        bool __pressRegexResult = Regex.Match(press, __pressPattern, RegexOptions.IgnoreCase).Success;

        if(__pressRegexResult)
        {
            ReturnDaily(press, date);
            DailyCheck.Check1 = false;
        }
    }

    private void ReturnDaily(string press, string date)
    {
        if (TimeSpan.TryParse(date, out TimeSpan __parsedTime))
        {
            DateTime __currentDateTime = DateTime.Today.Add(__parsedTime);

            MaintenanceDB_Daily __dailyCheck = DataRetriever.Get<MaintenanceDB_Daily>(
            f => f.Press.ToLower() == press.ToLower() &&
                 f.Datecompleted.HasValue &&
                 f.Datecompleted.Value.Date == __currentDateTime.Date &&
                 f.Datecompleted.Value.TimeOfDay.Hours == __currentDateTime.TimeOfDay.Hours &&
                 f.Datecompleted.Value.TimeOfDay.Minutes == __currentDateTime.TimeOfDay.Minutes).FirstOrDefault()!;

            if (__dailyCheck != null)
            {
                DailyCheck = __dailyCheck;
            }
        }
    }
}