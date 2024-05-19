using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68454ec3-d160-4232-b843-b8bc78c8a103", "AQAAAAIAAYagAAAAEMZ3tCyrwh9fVN/w9JyCqeyCXLcsGTFt3dpvRtuhbNWKh9l9rTjOU0rrn9vhA+02pg==", "d4997f02-7916-48bf-9481-67f484672741" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ad78278-e36b-4ada-8616-bf914ea770f4", "AQAAAAIAAYagAAAAEPF7MLoW6essmM46RV7NYhob7ahfkDr2/cwTRATd7GaS0z3HOSc3kA2G/qbeauMMEw==", "117553a9-e334-485e-ac97-4384de483250" });
        }
    }
}
