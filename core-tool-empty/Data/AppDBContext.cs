using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using core_tool_empty.Data;

namespace core_tool_empty.Data
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Books> tblBooks { get; set; }

        public DbSet<core_tool_empty.Data.Login> Login { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=Books;Integrated Security=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
