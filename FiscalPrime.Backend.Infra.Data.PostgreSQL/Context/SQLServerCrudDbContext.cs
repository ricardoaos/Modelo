using FiscalPrime.Backend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.SqlServer.Context
{
    public class SQLServerCrudDbContext : FiscalPrimeDbContext
    {
        public SQLServerCrudDbContext(DbContextOptions<FiscalPrimeDbContext> options, ITnfSession session)
            : base(options, session)
        {
        }
    }
}