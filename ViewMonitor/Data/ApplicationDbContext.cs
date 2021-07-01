using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using ViewMonitor.Models;

namespace ViewMonitor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<Monitor_Estado> Monitor_Estados { get; set; }
        public DbSet<Job_Monitor> Job_Monitors { get; set; }
        public DbSet<Agrupacion> Agrupacions { get; set; }
        public DbSet<Monitor_Estado_Hist> Monitor_Estado_Hists { get; set; }
        public DbSet<ViewHistEstadoMonitor> ViewHistEstadoMonitors { get; set; }
        public DbSet<Monitor_Estado_Ultimo> Monitor_Estado_Ultimos {get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Monitor>()
                   .HasOne(a => a.Job_Monitor)
                   .WithMany(a => a.Monitors)
                   .HasForeignKey(a => a.Job_MonitorID);

            builder.Entity<Monitor>()
                   .HasOne(a => a.Agrupacion)
                   .WithMany(a => a.Monitors)
                   .HasForeignKey(a => a.AgrupacionID);

            builder.Entity<Monitor_Estado_Hist>()
                   .HasOne(a => a.Monitor)
                   .WithMany(a => a.Monitor_Estado_Hists)
                   .HasForeignKey(a => a.MonitorID);

            builder.Entity<Monitor>()
                   .HasOne(a => a.Monitor_Estado)
                   .WithOne(a => a.Monitor)
                   .HasForeignKey<Monitor_Estado>(a => a.MonitorID);

            builder.Entity<ViewHistEstadoMonitor>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("view_hist_estado_monitor");
            });

            builder.Entity<Monitor>()
                   .HasOne(a => a.Monitor_Estado_Ultimo)
                   .WithOne(a => a.Monitor)
                   .HasForeignKey<Monitor_Estado_Ultimo>(a => a.MonitorID);

            base.OnModelCreating(builder);
        }
    }
}