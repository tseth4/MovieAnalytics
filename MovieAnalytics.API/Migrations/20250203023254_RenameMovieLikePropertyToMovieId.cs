using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAnalytics.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameMovieLikePropertyToMovieId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieLikes_Movies_LikedMovieId",
                table: "MovieLikes");

            migrationBuilder.RenameColumn(
                name: "LikedMovieId",
                table: "MovieLikes",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieLikes_LikedMovieId",
                table: "MovieLikes",
                newName: "IX_MovieLikes_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieLikes_Movies_MovieId",
                table: "MovieLikes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieLikes_Movies_MovieId",
                table: "MovieLikes");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MovieLikes",
                newName: "LikedMovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieLikes_MovieId",
                table: "MovieLikes",
                newName: "IX_MovieLikes_LikedMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieLikes_Movies_LikedMovieId",
                table: "MovieLikes",
                column: "LikedMovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
