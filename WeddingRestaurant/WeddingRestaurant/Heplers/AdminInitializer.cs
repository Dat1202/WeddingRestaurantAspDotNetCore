using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Threading.Tasks;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Helpers
{
	public class AdminInitializer : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			var hasher = new PasswordHasher<ApplicationUser>();

			var adminUser = new ApplicationUser
			{
				Id = Guid.NewGuid().ToString(), // Sử dụng một ID ngẫu nhiên
				UserName = "admin@example.com",
				NormalizedUserName = "admin@example.com".ToUpper(),
				Email = "admin@example.com",
				NormalizedEmail = "admin@example.com".ToUpper(),
				EmailConfirmed = true,
				Avatar = "default.jpg",
				PasswordHash = hasher.HashPassword(null, "Admin123")
			};

			builder.HasData(adminUser);

			var adminRoleId = "1"; // ID của role "Admin"
			var userRole = new IdentityUserRole<string>
			{
				UserId = adminUser.Id,
				RoleId = adminRoleId
			};

			builder.HasData(userRole);
		}
	}
}
