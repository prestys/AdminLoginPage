using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagonID.Data.Models;

public partial class MaintenanceDB_DailyCoater : BaseEntity
{
    public string? Employee { get; set; }
    public string? Coater { get; set; }
    public DateTime? DateCompleted { get; set; }
    public bool? Check1 { get; set; }
    public bool? Check2 { get; set; }
    public bool? Check3 { get; set; }
    public bool? Check4 { get; set; }
    public string? Comment { get; set; }
    [Column("Deleted?")]
    public string? Deleted { get; set; }
    public bool? Check5 { get; set; }
    public bool? Check6 { get; set; }
}
