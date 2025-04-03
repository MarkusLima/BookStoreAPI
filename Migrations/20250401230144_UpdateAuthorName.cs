using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuthorName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_autorId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "autorId",
                table: "Books",
                newName: "authorId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_autorId",
                table: "Books",
                newName: "IX_Books_authorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_authorId",
                table: "Books",
                column: "authorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_authorId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "authorId",
                table: "Books",
                newName: "autorId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_authorId",
                table: "Books",
                newName: "IX_Books_autorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_autorId",
                table: "Books",
                column: "autorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
