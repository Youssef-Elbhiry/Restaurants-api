
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantDbContext : IdentityDbContext<User>
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishs { get; set; }

    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);
        modelBuilder.Entity<Restaurant>()
          .HasMany(r => r.Dishes).WithOne()
          .HasForeignKey(d => d.RestaurantId);       


        modelBuilder.Entity<User>()
            .HasMany(u=>u.OwnedRestaurants)
            .WithOne(r=>r.Owner)
        .HasForeignKey(r=>r.OwnerId);

    }
}
