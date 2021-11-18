using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RailwayApp.Models;

namespace RailwayApp.Data
{
    public class AppDataContext:DbContext
    {
        public AppDataContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
