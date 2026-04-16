using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWatchlistAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchlistItem_AspNetUsers_UserId1",
                table: "WatchlistItem");

            migrationBuilder.DropIndex(
                name: "IX_WatchlistItem_UserId1",
                table: "WatchlistItem");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "WatchlistItem");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WatchlistItem",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "WatchlistItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistItem_MovieId",
                table: "WatchlistItem",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistItem_UserId",
                table: "WatchlistItem",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchlistItem_AspNetUsers_UserId",
                table: "WatchlistItem",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchlistItem_Movies_MovieId",
                table: "WatchlistItem",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchlistItem_AspNetUsers_UserId",
                table: "WatchlistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchlistItem_Movies_MovieId",
                table: "WatchlistItem");

            migrationBuilder.DropIndex(
                name: "IX_WatchlistItem_MovieId",
                table: "WatchlistItem");

            migrationBuilder.DropIndex(
                name: "IX_WatchlistItem_UserId",
                table: "WatchlistItem");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "WatchlistItem");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WatchlistItem",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "WatchlistItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistItem_UserId1",
                table: "WatchlistItem",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchlistItem_AspNetUsers_UserId1",
                table: "WatchlistItem",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
