using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OOPTut.Core.Users;

namespace OOPTut.EntityFramework.Contexts
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options):base(options)
        {

        }

        // Veritabanina eklenecek tablolar icin tanimlanan siniflar buraya gelecek:



    }
}
