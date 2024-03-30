using Domain.Models.DTOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DataContext
{
    public class ApiContext : IdentityDbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=NOTE_MONSTER;User ID=dev;Password=dev;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //    base.OnConfiguring(optionsBuilder);
        //}
        public DbSet<UserDTO> MyUsers { get; set; } = default!;
    }
}
