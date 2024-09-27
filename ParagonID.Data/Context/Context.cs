using Microsoft.EntityFrameworkCore;
using ParagonID.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace ParagonID.Data.Context;

public partial class Context : DbContext
{
    public Context() { }

    public Context(DbContextOptions<Context> options) : base(options) { }

    public virtual DbSet<MaintenanceDB_PressSpec> MaintenanceDB_PressSpecs { get; set; }
    public virtual DbSet<MaintenanceDB_Daily> MaintenanceDB_Dailies { get; set; }
    public virtual DbSet<MaintenanceDB_DailyCoater> MaintenanceDB_DailyCoaters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        optionsBuilder.UseSqlServer("InternalConnectionString");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaintenanceDB_PressSpec>(entity =>
        {
            entity.HasKey(e => e.UUID);
            entity.ToTable("MaintenanceDB_PressSpec", "dbo");
            entity.Property(e => e.UUID)
            .HasColumnName("ID");
            entity.Property(e => e.Press);
            entity.Property(e => e.Stops);
            entity.Property(e => e.Guards);
            entity.Property(e => e.Inkjets);
            entity.Property(e => e.Uv);
            entity.Property(e => e.Fanfold);
        });

        modelBuilder.Entity<MaintenanceDB_Daily>(entity =>
        {
            entity.HasKey(e => e.UUID);
            entity.ToTable("MaintenanceDB_Daily", "dbo");
            entity.Property(e => e.UUID)
            .HasColumnName("ID");
            entity.Property(e => e.Employee);
            entity.Property(e => e.Press);
            entity.Property(e => e.Datecompleted);
            entity.Property(e => e.Check1);
            entity.Property(e => e.Check2);
            entity.Property(e => e.Check3);
            entity.Property(e => e.Check4);
            entity.Property(e => e.Comment);
            entity.Property(e => e.Deleted);
            entity.Property(e => e.Check5);
            entity.Property(e => e.Check6);
        });

        modelBuilder.Entity<MaintenanceDB_DailyCoater>(entity =>
        {
            entity.HasKey(e => e.UUID);
            entity.ToTable("MaintenanceDB_DailyCoater", "dbo");
            entity.Property(e => e.UUID)
            .HasColumnName("ID");
            entity.Property(e => e.Employee);
            entity.Property(e => e.Coater);
            entity.Property(e => e.DateCompleted);
            entity.Property(e => e.Check1);
            entity.Property(e => e.Check2);
            entity.Property(e => e.Check3);
            entity.Property(e => e.Check4);
            entity.Property(e => e.Comment);
            entity.Property(e => e.Deleted);
            entity.Property(e => e.Check5);
            entity.Property(e => e.Check6);
        });
    }
}
