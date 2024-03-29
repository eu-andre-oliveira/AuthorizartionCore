using Domain.Models.Authorizations;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AuthorizationDbContext : DbContext
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> opt) : base(opt)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("\"Data Source=NOTE_MONSTER;User ID=dev;Password=dev;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False\"");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<RegisterUserRequest> Users { get; set; }
    }
}
