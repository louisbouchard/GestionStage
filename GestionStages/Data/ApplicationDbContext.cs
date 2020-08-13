using System;
using System.Collections.Generic;
using System.Text;
using GestionStages.Models.MilieuStage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionStages.Data
{
    // https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs
    // https://www.learnentityframeworkcore.com
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Entreprise> Entreprises { get; set; }

        public DbSet<TypeEntreprise> TypesEntreprise { get; set; }

        public DbSet<TypeMilieuStage> TypesMilieuxStage { get; set; }

        public DbSet<EntrepriseTypeMilieuStage> EntreprisesTypesMilieuxStage { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntrepriseTypeMilieuStage>()
                .HasKey(cle => new { cle.EntrepriseTypeMilieuStageId });
                //.HasKey(cle => new { cle.EntrepriseId, cle.TypeMilieuStageId });

            modelBuilder.Entity<EntrepriseTypeMilieuStage>()
                .HasOne(cle => cle.Entreprises)
                .WithMany(cle => cle.EntreprisesTypesMilieuxStage)
                .HasForeignKey(cle => cle.EntrepriseId);

            modelBuilder.Entity<EntrepriseTypeMilieuStage>()
                .HasOne(cle => cle.TypesMilieuxStage)
                .WithMany(cle => cle.EntreprisesTypesMilieuxStage)
                .HasForeignKey(cle => cle.TypeMilieuStageId);
        }
    }
}
