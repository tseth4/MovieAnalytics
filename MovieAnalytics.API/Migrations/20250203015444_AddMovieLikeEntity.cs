using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAnalytics.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieLikeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieLikes",
                columns: table => new
                {
                    SourceUserId = table.Column<string>(type: "TEXT", nullable: false),
                    LikedMovieId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieLikes", x => new { x.SourceUserId, x.LikedMovieId });
                    table.ForeignKey(
                        name: "FK_MovieLikes_AspNetUsers_SourceUserId",
                        column: x => x.SourceUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieLikes_Movies_LikedMovieId",
                        column: x => x.LikedMovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieLikes_LikedMovieId",
                table: "MovieLikes",
                column: "LikedMovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieLikes");
        }
    }
}
