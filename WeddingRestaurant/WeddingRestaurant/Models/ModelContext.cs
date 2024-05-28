﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;
using WeddingRestaurant.Helpers;
using System.Reflection.Emit;

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
            builder.Entity<ChatMessage>()
               .HasOne(m => m.Sender)
               .WithMany()
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ChatMessage>()
                .HasOne(m => m.Recipient)
                .WithMany()
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>()
			   .HasOne(p => p.Category)
			   .WithMany()
			   .HasForeignKey(p => p.CategoryId)
			   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Menu>()
               .HasOne(p => p.TypeMenu)
               .WithMany()
               .HasForeignKey(p => p.TypeID)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MenuProduct>()
               .HasOne(p => p.Product)
               .WithMany()
               .HasForeignKey(p => p.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MenuProduct>()
                .HasOne(p => p.Menu)
                .WithMany()
                .HasForeignKey(p => p.MenuId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Event>()
              .HasOne(p => p.Room)
              .WithMany()
              .HasForeignKey(p => p.RoomId)
              .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<ChatMessage> ChatMessage { get; set; }
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
