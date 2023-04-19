using Detyre1PMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Detyre1PMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Kategoria> Kategorite { get; set; }
        public virtual DbSet<Libri> Librat { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Test> Tests { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}