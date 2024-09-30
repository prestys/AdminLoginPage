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
    public IList<Machine> Machines = new List<Machine>();
    public bool IsLoading = true;
    //

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IEnumerable<MaintenanceDB_PressSpec> __machines = DataRetriever.GetAll<MaintenanceDB_PressSpec>();
            MachineSorter(__machines);
            IsLoading = false;
            StateHasChanged();
        }

        return base.OnAfterRenderAsync(firstRender);
    }

    /// <summary>
    /// Takes in a number of machines and directs them to the correct methods whether it be
    /// for formatting a press or a differnt machine.
    /// </summary>
    /// <param name="machines"></param>
    private void MachineSorter(IEnumerable<MaintenanceDB_PressSpec> machines)
    {
        string __pressRegex = @"^p\d{1,2}$";
        DateTime __today = DateTime.Now;
        DateTime __weekAgo = __today.AddDays(-7);
        DateTime __monthAgo = __today.AddMonths(-1);

        foreach (var __machine in machines)
        {
            Machine __newMachine = new Machine();

            if (Regex.Match(__machine.Press, __pressRegex, RegexOptions.IgnoreCase).Success)
            {
                __newMachine = FormatPress(__machine, __today, __weekAgo, __monthAgo);
                __newMachine.Name = __machine.Press;
            } else
            {
                __newMachine = FormatMachines(__machine, __today, __weekAgo, __monthAgo);
                __newMachine.Name = __machine.Press;
            }

            Machines.Add(__newMachine);
        }
    }

    /// <summary>
    /// This formats any press that gets passed through into the Machine class
    /// and returns them.
    /// </summary>
    /// <param name="press"></param>
    /// <param name="today"></param>
    /// <param name="weekAgo"></param>
    /// <param name="monthAgo"></param>
    /// <returns></returns>
    private Machine FormatPress(MaintenanceDB_PressSpec press, DateTime today, DateTime weekAgo, DateTime monthAgo)
    {
        Machine __newPress = new Machine();

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

    /// <summary>
    /// This formats all other machines other than a press into the 
    /// Machine class.
    /// </summary>
    /// <param name="press"></param>
    /// <param name="today"></param>
    /// <param name="weekAgo"></param>
    /// <param name="monthAgo"></param>
    /// <returns></returns>
    private Machine FormatMachines(MaintenanceDB_PressSpec press, DateTime today, DateTime weekAgo, DateTime monthAgo)
    {
        Machine __newPress = new Machine();

        IEnumerable<MaintenanceDB_DailyCoater> __daily = DataRetriever.Get<MaintenanceDB_DailyCoater>(
        f => f.DateCompleted.HasValue &&
                f.DateCompleted.Value.Date == today.Date &&
                string.Equals(f.Coater, press.Press, StringComparison.OrdinalIgnoreCase));

        if (__daily.Any())
        {
            __newPress.Daily = __daily.First().DateCompleted?.ToString("t")!;
        }

        IEnumerable<MaintenanceDB_DailyCoater> __weekly = DataRetriever.Get<MaintenanceDB_DailyCoater>(
            f => f.DateCompleted.HasValue &&
                    f.DateCompleted.Value.Date >= weekAgo.Date &&
                    f.DateCompleted.Value.Date <= today.Date &&
                    string.Equals(f.Coater, press.Press, StringComparison.OrdinalIgnoreCase));

        if (__weekly.Any())
        {
            __newPress.Weekly = __weekly.First().DateCompleted?.ToString("d")!;
        }

        IEnumerable<MaintenanceDB_DailyCoater> __monthly = DataRetriever.Get<MaintenanceDB_DailyCoater>(
            f => f.DateCompleted.HasValue &&
                    f.DateCompleted.Value.Date >= monthAgo.Date &&
                    f.DateCompleted.Value.Date <= today.Date &&
                    string.Equals(f.Coater, press.Press, StringComparison.OrdinalIgnoreCase));

        if (__monthly.Any())
        {
            __newPress.Monthly = __monthly.First().DateCompleted?.ToString("d")!;
        }

        return __newPress;
    }
}
