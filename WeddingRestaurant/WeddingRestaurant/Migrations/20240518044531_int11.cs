using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class int11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Rooms",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc5d9e71-4ef0-415a-9242-2e9b2805f0a4", "AQAAAAIAAYagAAAAEFuZRTkmIjvU2agf18c1lh563pwV/wksuBGFTXYiiilpygiQ3DoG5fECjIJVKqQihQ==", "2748a09f-0c8e-45fd-8ac9-efa4bab95117" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68454ec3-d160-4232-b843-b8bc78c8a103", "AQAAAAIAAYagAAAAEMZ3tCyrwh9fVN/w9JyCqeyCXLcsGTFt3dpvRtuhbNWKh9l9rTjOU0rrn9vhA+02pg==", "d4997f02-7916-48bf-9481-67f484672741" });
        }
    }
}
