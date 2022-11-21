using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mikro.Task.Services.Domain;

namespace Mikro.Task.Services.Db
{
    public class IdDbContext : IdentityDbContext<UserModel>
    {
        public IdDbContext(DbContextOptions<IdDbContext> options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
    }
}
