using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OOPTut.Core.Bazaar;
using OOPTut.Core.Navbar;
using OOPTut.Core.Users;

namespace OOPTut.EntityFramework.Contexts
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {
        }

        // Veritabanina eklenecek tablolar icin tanimlanan model siniflari buraya gelecek:
        public DbSet<BazaarList> BazaarLists { get; set; }
        public DbSet<BazaarListItem> BazaarListItems { get; set; }
        public DbSet<NavBarMenuItem> NavBarMenuItems { get; set; }
        public DbSet<SharedBazaarList> SharedBazaarLists { get; set; }
    }
}
