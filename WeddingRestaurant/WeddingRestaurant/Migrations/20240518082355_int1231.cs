using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class int1231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Rooms",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0bccb86-53c6-4311-bb23-57ee16785582", "AQAAAAIAAYagAAAAEHF5+20OVh+pbAiZ7FJ4nrkLPwEEzo0JeACvSkJWSA/lQ8Mp0nbmnqSIFyHflQPnTg==", "31e39a7e-770d-42b5-b666-22d7b17e21e5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Rooms",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78cdfebe-f600-4b70-994e-db8a70d3c826", "AQAAAAIAAYagAAAAEPJXsaRhRTmh/ohO7zwLD685jeXuO71ALDDQ+CD3tvVwL7k1/b6Jh56wmLDVXwgv2Q==", "7b0f616b-362d-4534-97c5-f4aad072fc27" });
        }
    }
}
