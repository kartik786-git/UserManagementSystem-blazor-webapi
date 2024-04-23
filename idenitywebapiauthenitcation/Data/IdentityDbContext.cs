using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using idenitywebapiauthenitcation.Entity;

namespace idenitywebapiauthenitcation.Data
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
            
        }

        public DbSet<Blog>  Blogs { get; set; }
    }
}
