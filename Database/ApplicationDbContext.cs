using WebApplication2.ApiEndPoints;
using WebApplication2.Models;

namespace WebApplication2.Database;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

   
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }

}
