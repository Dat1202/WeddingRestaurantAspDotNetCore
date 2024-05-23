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
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "ChatMessage",
                newName: "Content");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5a6814a-692b-4045-b471-078fa984a19c", "AQAAAAIAAYagAAAAEFUEinqK7yjNZpSNMPthQrPY0gitF5UzhGdeT2d7qIwlG0sVcm1W3k3nWOUGHsjdeg==", "55defbe9-5949-402c-9149-c804460d79a1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "ChatMessage",
                newName: "Message");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d594c16-765e-49f7-a8a6-f4b0b47660f0", "AQAAAAIAAYagAAAAEL++x5MAoht34uJprkpXIfsC7mMp3vK7Pp9wLkviwLmwV0Ut7PIPqqqArYMaBg0jEQ==", "e8d9b05e-59e4-4a91-915e-f8b302791c8b" });
        }
    }
}
