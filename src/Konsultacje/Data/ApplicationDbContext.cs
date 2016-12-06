using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Konsultacje.Models;

namespace Konsultacje.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .Property(p => p.DisplayName)
                .HasComputedColumnSql("[Imie] + ' ' + [Nazwisko]");

            builder.Entity<ZapisNaKonsultacje>()
                .HasOne(p => p.Student)
                .WithMany(b => b.ZapisNaKonsultacje)
                .HasForeignKey(p => p.StudentID)
                .HasConstraintName("ForeignKey_Zapis_Student");

            builder.Entity<ZapisNaKonsultacje>()
                .HasOne(p => p.Konsultacja)
                .WithMany(b => b.ZapisNaKonsultacje)
                .HasForeignKey(p => p.KonsultacjaID)
                .HasConstraintName("ForeignKey_Zapis_Konsultacja");

            builder.Entity<Konsultacja>()
                .HasOne(p => p.PracownikUczelni)
                .WithMany(b => b.Konsultacje)
                .HasForeignKey(p => p.PracownikUczelniID)
                .HasConstraintName("ForeignKey_Konsultacja_Pracownik");
        }

        public DbSet<Konsultacja> Konsultacja { get; set; }

        public DbSet<ZapisNaKonsultacje> ZapisNaKonsultacje { get; set; }
    }
}
