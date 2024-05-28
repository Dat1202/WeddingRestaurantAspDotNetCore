using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class aefwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d506ba77-237b-41c5-8280-1a51ff334cd2", "AQAAAAIAAYagAAAAEJG+cyWGowKt89KepCOmoqWa0wTHJTDD8HqTBDV08W95pouTI9IEu4Z5dNZDPVoq7w==", "f0d8d4c8-8855-4c8a-be51-22df7d3e2b56" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c680eab-0ee3-459f-983b-d5418515d180", "AQAAAAIAAYagAAAAEG31hSJROQbZ5Zs/PxRgBdIwKZSP7eFxz1sgyWi4/t0lhZHbBa6thj7b1KPyNV00kg==", "4c3ec4cf-6650-4b76-9a6d-b15688aed34d" });
        }
    }
}
