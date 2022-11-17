using Microsoft.EntityFrameworkCore;
using SerhatYas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerhatYas.DataAccess.Concrete.EntityFramework
{
    public class SerhatYasDB:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB;database=projectDB;uid=sa;pwd=123");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProjectMap> UserProjectMaps { get; set; }
    }
}
