using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpreendedoresApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpreendedoresApp.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sistema.db");
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
