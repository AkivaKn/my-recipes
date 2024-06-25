using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRecipes.Migrations
{
    /// <inheritdoc />
    public partial class applicationuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_User_UserId",
                table: "Dishes");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_AspNetUsers_UserId",
                table: "Dishes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_AspNetUsers_UserId",
                table: "Dishes");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_User_UserId",
                table: "Dishes",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
