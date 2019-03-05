using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarAccessories.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Type { get; set; }

        public virtual Seller Seller { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<Rate> CustomerRate { get; set; }
        public virtual ICollection<Rate> SellerRate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //"Data Source=.; Initial Catalog=ITISystem; Integrated Security=True"
        public ApplicationDbContext()
            : base("Data Source=.; Initial Catalog=CarAccessories; User id=sa; Password=rootroot;", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Description> Descriptions { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}