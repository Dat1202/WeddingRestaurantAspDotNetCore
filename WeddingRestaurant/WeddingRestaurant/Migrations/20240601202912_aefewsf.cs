using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class aefewsf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_ApplicationUser",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ApplicationUser",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ApplicationUser",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "577109cf-3701-48e7-adfa-97fc1be17eca", "AQAAAAIAAYagAAAAEHsZgLsKtgIlSCH1sHDryvHf6yjQjH1UsZXZPbdNC5JrreV7EglSOK84RNVH69tiDA==", "ca244f53-ef1a-41d5-9d9a-352201340995" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrderId",
                table: "Events",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Orders_OrderId",
                table: "Events",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Orders_OrderId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_OrderId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser",
                table: "Events",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d506ba77-237b-41c5-8280-1a51ff334cd2", "AQAAAAIAAYagAAAAEJG+cyWGowKt89KepCOmoqWa0wTHJTDD8HqTBDV08W95pouTI9IEu4Z5dNZDPVoq7w==", "f0d8d4c8-8855-4c8a-be51-22df7d3e2b56" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_ApplicationUser",
                table: "Events",
                column: "ApplicationUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_ApplicationUser",
                table: "Events",
                column: "ApplicationUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
