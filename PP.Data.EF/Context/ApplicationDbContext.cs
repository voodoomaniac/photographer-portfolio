using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PP.Core.Entities;
using PP.Data.EF.Identity;

namespace PP.Data.EF.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Image> Images { get; set; }
    }
}