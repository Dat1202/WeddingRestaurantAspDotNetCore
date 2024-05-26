using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class àes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6573e814-10d4-480c-99f5-269ad718fe17", "AQAAAAIAAYagAAAAEDmqgOcUWxXf78dCNiMz8PmAUsWEjPEaVeyHD/IurE861bAZpgN2dDZ3czIgN2/skg==", "0deed3e9-87a4-49a4-83ea-f55083fb1b57" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e17e2726-4db7-4934-b441-c9580fba3e02", "AQAAAAIAAYagAAAAEDWSCnFvR0YEtNhzvpAKePydF/24dAaVG+2w3ekHGTPlP4gKydM/XsEoHx1w0n80Ew==", "ed42995f-e16a-47ad-a976-21c47acad645" });
        }
    }
}
