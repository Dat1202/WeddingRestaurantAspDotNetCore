using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeddingRestaurant.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Duration> Durations { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<RentCost> RentCosts { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<MenuProduct> MenuProducts { get; set; }


}
