using EnvantoryManagement.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnvantoryManagement.Data;
public class AppDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Tag> ItemTags { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<Location> Locations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}
