using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;
using WeddingRestaurant.Helpers;

namespace WeddingRestaurant.Models
{
	public partial class ModelContext : IdentityDbContext<ApplicationUser>
	{
		public ModelContext(DbContextOptions<ModelContext> options)
			: base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration(new SeedData() as IEntityTypeConfiguration<ApplicationUser>);
			builder.ApplyConfiguration(new SeedData() as IEntityTypeConfiguration<IdentityRole>);
			builder.ApplyConfiguration(new SeedData() as IEntityTypeConfiguration<IdentityUserRole<string>>);

			//builder.ApplyConfiguration(new AdminInitializer());
		}

		public DbSet<ApplicationUser> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Category> Categories { get; set; }
		//public DbSet<Duration> Durations { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Menu> Menus { get; set; }
		//public DbSet<RentCost> RentCosts { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<MenuProduct> MenuProducts { get; set; }
		public DbSet<TypeMenu> TypeMenus { get; set; }
	}
}
