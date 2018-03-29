using Microsoft.EntityFrameworkCore;
using VirtualTablesTest.Models;
using VirtualTablesTest.Services;

namespace VirtualTablesTest
{
    public class WorkTestDbContext: DbContext, IDbContext
    {
        public DbSet<VirtualTable> Tables { get; set; }
        public DbSet<VirtualColumn> Columns { get; set; }
        public DbSet<VirtualRow> Rows { get; set; }
        public DbSet<VirtualValue> Values { get; set; }

        public WorkTestDbContext(DbContextOptions options): base(options) { }

    }
}