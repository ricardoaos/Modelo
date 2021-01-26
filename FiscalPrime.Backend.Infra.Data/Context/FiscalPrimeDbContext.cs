using FiscalPrime.Backend.Domain.Entities.DominioExterno;
using FiscalPrime.Backend.Domain.Entities.Empresas;
using FiscalPrime.Backend.Infra.Data.Mappers.Entities;
using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace FiscalPrime.Backend.Infra.Data.Context
{
    public abstract class FiscalPrimeDbContext : TnfDbContext
    {

        #region Propriedades

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<DominioExterno> DominioExterno { get; set; }

        #endregion

        #region Construtor

        // Importante o construtor do contexto receber as opções com o tipo generico definido: DbContextOptions<TDbContext>
        public FiscalPrimeDbContext(DbContextOptions<FiscalPrimeDbContext> options, ITnfSession session)
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmpresaTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DominioExternoTypeConfiguration());
        }

        #endregion

    }
}
