using Microsoft.AspNetCore.Components;
using ParagonID.Data.Models;
using ParagonID.Data.Service;
using ParagonID.InternalSystem.Models;
using System.Text.RegularExpressions;

namespace ParagonID.InternalSystem.Pages.Presses;

public class DashboardModel : ComponentBase
{
    // [Injections]
    [Inject] public DataRetriever DataRetriever { get; set; }
    //

    // [Properties]
    public IList<Press> Presses = new List<Press>();
    //

    protected override Task OnInitializedAsync()
    {
        IEnumerable<MaintenanceDB_PressSpec> __presses = DataRetriever.GetAll<MaintenanceDB_PressSpec>();
        PressFormat(__presses);

        return base.OnInitializedAsync();
    }

    private void PressFormat(IEnumerable<MaintenanceDB_PressSpec> presses)
    {
        string __date = DateTime.Now.ToString("d");
        string __pressRegex = @"^p\d{1,2}$";

        foreach (var __press in presses)
        {
            Press __newPress = new Press();

            if (Regex.Match(__press.Press, __pressRegex, RegexOptions.IgnoreCase).Success)
            {
                IEnumerable<MaintenanceDB_Daily> __daily = DataRetriever.GetAll<MaintenanceDB_Daily>();
                if(__daily != null)
                {
                    __newPress.Daily = __daily.FirstOrDefault()!.Datecompleted.ToString("d");
                }
            }

            Presses.Add(__newPress);
        }
    }
}
