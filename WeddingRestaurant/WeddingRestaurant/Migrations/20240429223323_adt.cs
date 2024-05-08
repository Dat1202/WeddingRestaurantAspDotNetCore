using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class adt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_UsersId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UsersId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Orders",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UsersId",
                table: "Orders",
                newName: "IX_Orders_User");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Events",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Events_UsersId",
                table: "Events",
                newName: "IX_Events_User");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_User",
                table: "Events",
                column: "User",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_User",
                table: "Orders",
                column: "User",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_User",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_User",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Orders",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_User",
                table: "Orders",
                newName: "IX_Orders_UsersId");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Events",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_User",
                table: "Events",
                newName: "IX_Events_UsersId");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_UsersId",
                table: "Events",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UsersId",
                table: "Orders",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
