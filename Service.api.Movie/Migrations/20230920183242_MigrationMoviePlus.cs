using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.api.Movie.Migrations
{
    /// <inheritdoc />
    public partial class MigrationMoviePlus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(300)", nullable: false),
                    ReleaseDate = table.Column<string>(type: "varchar(50)", nullable: false),
                    Director = table.Column<string>(type: "varchar(100)", nullable: false),
                    Genre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Rating = table.Column<string>(type: "varchar(50)", nullable: false),
                    Duration = table.Column<string>(type: "varchar(50)", nullable: false),
                    Language = table.Column<string>(type: "varchar(50)", nullable: false),
                    Country = table.Column<string>(type: "varchar(50)", nullable: false),
                    Budget = table.Column<int>(type: "int", nullable: false),
                    BoxOffice = table.Column<int>(type: "int", nullable: false),
                    ProductionCompany = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cast = table.Column<string>(type: "varchar(50)", nullable: false),
                    Plot = table.Column<string>(type: "varchar(600)", nullable: false),
                    PosterUrl = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    TrailerUrl = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Awards = table.Column<string>(type: "varchar(50)", nullable: false),
                    Keywords = table.Column<string>(type: "varchar(150)", nullable: false),
                    ImdbRating = table.Column<string>(type: "varchar(50)", nullable: false),
                    RottenTomatoesRating = table.Column<string>(type: "varchar(50)", nullable: false),
                    MetacriticRating = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
