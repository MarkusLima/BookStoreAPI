using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdPurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_userId",
                table: "Purchases",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_userId",
                table: "Purchases",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_userId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_userId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Purchases");
        }
    }
}
