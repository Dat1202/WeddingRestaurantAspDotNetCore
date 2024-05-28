using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class inti1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12277842-1210-43b4-8f05-3dff1626ce5e", "AQAAAAIAAYagAAAAEO6mTdPHJhbZLibD+4vBS8pz3dVQ7XH1/bwaCKNCk5/5Ht2WOnxvFXKHe4H7CFlGOw==", "a6ad9d00-6401-4f57-ad9c-ea8ccdd34d85" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8aaaec94-1118-4957-9a36-11057e4817e2", "AQAAAAIAAYagAAAAEJx3m9JNmFNGRiiGAuEbkc51gdGsJjHCp3tfpG5+KgcMjjd0gx1HPwspbDNzxyQxlw==", "8867e59e-9460-4951-a5ed-2062ccc77d4f" });
        }
    }
}
