using Daisy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Infrastructure
{
    public class DataContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DataContext() : base("name=DataContext")
        {
        }

        public DataContext(string connectionString) : base(connectionString)
        {
        }

        #region DbSet
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Album> Albums { get; set; }
        #endregion DbSet
    }
}
