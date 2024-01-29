using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudProduct.Models;
using CrudStudent.Models;

namespace CrudStudent.Data
{
    public class CrudStudentContext : DbContext
    {
        public CrudStudentContext (DbContextOptions<CrudStudentContext> options)
            : base(options)
        {
        }
        
        public DbSet<CrudProduct.Models.Product> Product { get; set; } = default!;
        public DbSet<CrudProduct.Models.Category> Categories { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasNoKey();
        }
        public DbSet<CrudStudent.Models.Customer> Customer { get; set; } = default!;
    }
}
