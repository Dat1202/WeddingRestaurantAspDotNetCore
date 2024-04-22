using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMenu", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menus_TypeID",
                table: "Menus",
                column: "TypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_TypeMenu_TypeID",
                table: "Menus",
                column: "TypeID",
                principalTable: "TypeMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_TypeMenu_TypeID",
                table: "Menus");

            migrationBuilder.DropTable(
                name: "TypeMenu");

            migrationBuilder.DropIndex(
                name: "IX_Menus_TypeID",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "Menus");
        }
    }
}
