using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class inti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8aaaec94-1118-4957-9a36-11057e4817e2", "AQAAAAIAAYagAAAAEJx3m9JNmFNGRiiGAuEbkc51gdGsJjHCp3tfpG5+KgcMjjd0gx1HPwspbDNzxyQxlw==", "8867e59e-9460-4951-a5ed-2062ccc77d4f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8d2a614-d20d-4bdb-95fa-98e395e1ab77", "AQAAAAIAAYagAAAAEGIHRgqAYC1GXLpL9k96BDoQ8i89g8ihQavJCc2/HA+ghY+9X96IiMNpOlrIqYBJww==", "eb6d2b51-6a05-438a-8371-a9fc43442964" });
        }
    }
}
