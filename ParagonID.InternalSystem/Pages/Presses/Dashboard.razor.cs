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
        IEnumerable<MaintenanceDB_PressSpec> __machines = DataRetriever.GetAll<MaintenanceDB_PressSpec>();
        MachineFormat(__machines);

        return base.OnInitializedAsync();
    }

    private void MachineFormat(IEnumerable<MaintenanceDB_PressSpec> machines)
    {
        string __pressRegex = @"^p\d{1,2}$";
        DateTime __today = DateTime.Now;
        DateTime __weekAgo = __today.AddDays(-7);
        DateTime __monthAgo = __today.AddMonths(-1);

        foreach (var __machine in machines)
        {
            Press __newPress = new Press();

            if (Regex.Match(__machine.Press, __pressRegex, RegexOptions.IgnoreCase).Success)
            {
                __newPress = FormatPress(__machine, __today, __weekAgo, __monthAgo);
                __newPress.Name = __machine.Press;
            }

            Presses.Add(__newPress);
        }
    }

    private Press FormatPress(MaintenanceDB_PressSpec press, DateTime today, DateTime weekAgo, DateTime monthAgo)
    {
        Press __newPress = new Press();

        IEnumerable<MaintenanceDB_Daily> __daily = DataRetriever.Get<MaintenanceDB_Daily>(
        f => f.Datecompleted.HasValue &&
                f.Datecompleted.Value.Date == today.Date &&
                string.Equals(f.Press, press.Press, StringComparison.OrdinalIgnoreCase));

        if (__daily.Any())
        {
            __newPress.Daily = __daily.First().Datecompleted?.ToString("t")!;
        }

        IEnumerable<MaintenanceDB_Daily> __weekly = DataRetriever.Get<MaintenanceDB_Daily>(
            f => f.Datecompleted.HasValue &&
                    f.Datecompleted.Value.Date >= weekAgo.Date &&
                    f.Datecompleted.Value.Date <= today.Date &&
                    string.Equals(f.Press, press.Press, StringComparison.OrdinalIgnoreCase));

        if (__weekly.Any())
        {
            __newPress.Weekly = __weekly.First().Datecompleted?.ToString("d")!;
        }

        IEnumerable<MaintenanceDB_Daily> __monthly = DataRetriever.Get<MaintenanceDB_Daily>(
            f => f.Datecompleted.HasValue &&
                    f.Datecompleted.Value.Date >= monthAgo.Date &&
                    f.Datecompleted.Value.Date <= today.Date &&
                    string.Equals(f.Press, press.Press, StringComparison.OrdinalIgnoreCase));

        if (__monthly.Any())
        {
            __newPress.Monthly = __monthly.First().Datecompleted?.ToString("d")!;
        }

        return __newPress;
    }
}
