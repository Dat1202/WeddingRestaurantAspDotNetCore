using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class adade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24eec990-c5b3-4658-9eab-fe1e09a0fee0", "AQAAAAIAAYagAAAAEMeX5e+RgjGkcpgvvDsNnUzXoe3JxJ5OyX3iWjppaBltveDoGZH9/KNlq26cZnPOiA==", "42349461-30a7-4803-8c70-2f14e244e2da" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0bccb86-53c6-4311-bb23-57ee16785582", "AQAAAAIAAYagAAAAEHF5+20OVh+pbAiZ7FJ4nrkLPwEEzo0JeACvSkJWSA/lQ8Mp0nbmnqSIFyHflQPnTg==", "31e39a7e-770d-42b5-b666-22d7b17e21e5" });
        }
    }
}
