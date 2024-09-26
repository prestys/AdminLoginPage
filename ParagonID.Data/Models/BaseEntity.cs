using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagonID.Data.Models;

public interface IBaseEntity
{
    [Key]
    Guid UUID { get; set; }
    Guid ConcurrencyStamp { get; set; }
    DateTime? DateModified { get; set; }
    DateTime DateCreated { get; set; }
}

public class BaseEntity : IBaseEntity
{
    [Key] public Guid UUID { get; set; }
    [NotMapped] public Guid ConcurrencyStamp { get; set; }
    [NotMapped] public DateTime? DateModified { get; set; }
    [NotMapped] public DateTime DateCreated { get; set; }
}
