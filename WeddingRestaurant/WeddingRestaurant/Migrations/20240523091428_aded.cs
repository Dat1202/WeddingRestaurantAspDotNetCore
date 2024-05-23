using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class aded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "When",
                table: "ChatMessage",
                newName: "Time");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ChatMessage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ChatMessage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8d2a614-d20d-4bdb-95fa-98e395e1ab77", "AQAAAAIAAYagAAAAEGIHRgqAYC1GXLpL9k96BDoQ8i89g8ihQavJCc2/HA+ghY+9X96IiMNpOlrIqYBJww==", "eb6d2b51-6a05-438a-8371-a9fc43442964" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "ChatMessage",
                newName: "When");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ChatMessage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ChatMessage",
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
                values: new object[] { "e5a6814a-692b-4045-b471-078fa984a19c", "AQAAAAIAAYagAAAAEFUEinqK7yjNZpSNMPthQrPY0gitF5UzhGdeT2d7qIwlG0sVcm1W3k3nWOUGHsjdeg==", "55defbe9-5949-402c-9149-c804460d79a1" });
        }
    }
}
