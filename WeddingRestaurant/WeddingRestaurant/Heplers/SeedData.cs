using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Helpers
{
	public class SeedData : IEntityTypeConfiguration<ApplicationUser>, IEntityTypeConfiguration<IdentityRole>, IEntityTypeConfiguration<IdentityUserRole<string>>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			var hasher = new PasswordHasher<ApplicationUser>();

			var adminUser = new ApplicationUser
			{
				Id = "1", 
				UserName = "admin",
				NormalizedUserName = "admin".ToUpper(),
				Email = "admin@gmail.com",
				NormalizedEmail = "admin@gmail.com".ToUpper(),
				EmailConfirmed = true,
				Avatar = "",
				PasswordHash = hasher.HashPassword(null, "Admin123")
			};

			builder.HasData(adminUser);
		}

		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			var adminRole = new List<IdentityRole>
			{
				new IdentityRole
				{
					Id = "1",
					Name = "Admin",
					NormalizedName = "Admin".ToUpper()
				},
				new IdentityRole
				{
					Id = "2",
					Name = "User",
					NormalizedName = "User".ToUpper()
				}
			};

			builder.HasData(adminRole);
		}

		public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
		{
			var userRole = new IdentityUserRole<string>
			{
				UserId = "1", 
				RoleId = "1" 
			};

			builder.HasData(userRole);
		}
	}
}
