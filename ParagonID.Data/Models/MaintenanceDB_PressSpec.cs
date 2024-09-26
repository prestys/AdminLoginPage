using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagonID.Data.Models;

public partial class MaintenanceDB_PressSpec : BaseEntity
{
    public int ID { get; set; }
    public string Press { get; set; }
    public int Stops { get; set; }
    public int Guards { get; set; }
    public int Inkjets { get; set; }
    public int Uv { get; set; }
    public bool Fanfold { get; set; }
}
