using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class àes111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Rooms_RoomId",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c680eab-0ee3-459f-983b-d5418515d180", "AQAAAAIAAYagAAAAEG31hSJROQbZ5Zs/PxRgBdIwKZSP7eFxz1sgyWi4/t0lhZHbBa6thj7b1KPyNV00kg==", "4c3ec4cf-6650-4b76-9a6d-b15688aed34d" });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Rooms_RoomId",
                table: "Events",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Rooms_RoomId",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce548ce8-a9da-451a-b5f5-9c89b73f935c", "AQAAAAIAAYagAAAAEISV8jn5cR/XdcCfOQDhy25j1XCmn5YAiM6LjkkYJy9YPh8cTZkhBGcI1PXRcgCz5w==", "5fccb2fb-db7e-42cb-861c-3c36ab002019" });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Rooms_RoomId",
                table: "Events",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
