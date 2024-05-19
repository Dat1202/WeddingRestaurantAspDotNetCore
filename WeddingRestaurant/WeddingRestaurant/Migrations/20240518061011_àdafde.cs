using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class àdafde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78cdfebe-f600-4b70-994e-db8a70d3c826", "AQAAAAIAAYagAAAAEPJXsaRhRTmh/ohO7zwLD685jeXuO71ALDDQ+CD3tvVwL7k1/b6Jh56wmLDVXwgv2Q==", "7b0f616b-362d-4534-97c5-f4aad072fc27" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc5d9e71-4ef0-415a-9242-2e9b2805f0a4", "AQAAAAIAAYagAAAAEFuZRTkmIjvU2agf18c1lh563pwV/wksuBGFTXYiiilpygiQ3DoG5fECjIJVKqQihQ==", "2748a09f-0c8e-45fd-8ac9-efa4bab95117" });
        }
    }
}
