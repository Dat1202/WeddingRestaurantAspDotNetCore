using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class egr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e17e2726-4db7-4934-b441-c9580fba3e02", "AQAAAAIAAYagAAAAEDWSCnFvR0YEtNhzvpAKePydF/24dAaVG+2w3ekHGTPlP4gKydM/XsEoHx1w0n80Ew==", "ed42995f-e16a-47ad-a976-21c47acad645" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12277842-1210-43b4-8f05-3dff1626ce5e", "AQAAAAIAAYagAAAAEO6mTdPHJhbZLibD+4vBS8pz3dVQ7XH1/bwaCKNCk5/5Ht2WOnxvFXKHe4H7CFlGOw==", "a6ad9d00-6401-4f57-ad9c-ea8ccdd34d85" });
        }
    }
}
