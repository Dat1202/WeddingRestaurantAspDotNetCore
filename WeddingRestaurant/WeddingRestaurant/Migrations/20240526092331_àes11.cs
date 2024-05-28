using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class àes11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce548ce8-a9da-451a-b5f5-9c89b73f935c", "AQAAAAIAAYagAAAAEISV8jn5cR/XdcCfOQDhy25j1XCmn5YAiM6LjkkYJy9YPh8cTZkhBGcI1PXRcgCz5w==", "5fccb2fb-db7e-42cb-861c-3c36ab002019" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6573e814-10d4-480c-99f5-269ad718fe17", "AQAAAAIAAYagAAAAEDmqgOcUWxXf78dCNiMz8PmAUsWEjPEaVeyHD/IurE861bAZpgN2dDZ3czIgN2/skg==", "0deed3e9-87a4-49a4-83ea-f55083fb1b57" });
        }
    }
}
