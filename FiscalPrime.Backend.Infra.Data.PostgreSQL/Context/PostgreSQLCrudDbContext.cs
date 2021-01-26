using FiscalPrime.Backend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace FiscalPrime.Backend.Infra.Data.PostgreSQL.Context
{
    public class PostgreSQLCrudDbContext : FiscalPrimeDbContext
    {
        public PostgreSQLCrudDbContext(DbContextOptions<FiscalPrimeDbContext> options, ITnfSession session)
            : base(options, session)
        {
        }
    }
}
