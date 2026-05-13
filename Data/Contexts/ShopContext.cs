using Core.Concretes.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class ShopContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
    }
}
