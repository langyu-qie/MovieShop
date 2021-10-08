using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CreateFavoriteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "character",
                table: "MovieCast",
                newName: "Character");

            migrationBuilder.AddColumn<int>(
                name: "MovieDetailsId",
                table: "Trailer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieDetailsId",
                table: "MovieGenre",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieDetailsId",
                table: "MovieCast",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorite_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorite_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieDetailsModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tagline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PosterUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackdropUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RunTime = table.Column<int>(type: "int", nullable: true),
                    ImdbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TmdbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDetailsModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CastModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TmdbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Character = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieDetailsModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CastModel_MovieDetailsModel_MovieDetailsModelId",
                        column: x => x.MovieDetailsModelId,
                        principalTable: "MovieDetailsModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GenreModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieDetailsModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenreModel_MovieDetailsModel_MovieDetailsModelId",
                        column: x => x.MovieDetailsModelId,
                        principalTable: "MovieDetailsModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrailerModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrailerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    MovieDetailsModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailerModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrailerModel_MovieDetailsModel_MovieDetailsModelId",
                        column: x => x.MovieDetailsModelId,
                        principalTable: "MovieDetailsModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trailer_MovieDetailsId",
                table: "Trailer",
                column: "MovieDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_MovieDetailsId",
                table: "MovieGenre",
                column: "MovieDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCast_MovieDetailsId",
                table: "MovieCast",
                column: "MovieDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_CastModel_MovieDetailsModelId",
                table: "CastModel",
                column: "MovieDetailsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_MovieId",
                table: "Favorite",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_UserId",
                table: "Favorite",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreModel_MovieDetailsModelId",
                table: "GenreModel",
                column: "MovieDetailsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TrailerModel_MovieDetailsModelId",
                table: "TrailerModel",
                column: "MovieDetailsModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCast_MovieDetailsModel_MovieDetailsId",
                table: "MovieCast",
                column: "MovieDetailsId",
                principalTable: "MovieDetailsModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_MovieDetailsModel_MovieDetailsId",
                table: "MovieGenre",
                column: "MovieDetailsId",
                principalTable: "MovieDetailsModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trailer_MovieDetailsModel_MovieDetailsId",
                table: "Trailer",
                column: "MovieDetailsId",
                principalTable: "MovieDetailsModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCast_MovieDetailsModel_MovieDetailsId",
                table: "MovieCast");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_MovieDetailsModel_MovieDetailsId",
                table: "MovieGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_Trailer_MovieDetailsModel_MovieDetailsId",
                table: "Trailer");

            migrationBuilder.DropTable(
                name: "CastModel");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "GenreModel");

            migrationBuilder.DropTable(
                name: "TrailerModel");

            migrationBuilder.DropTable(
                name: "MovieDetailsModel");

            migrationBuilder.DropIndex(
                name: "IX_Trailer_MovieDetailsId",
                table: "Trailer");

            migrationBuilder.DropIndex(
                name: "IX_MovieGenre_MovieDetailsId",
                table: "MovieGenre");

            migrationBuilder.DropIndex(
                name: "IX_MovieCast_MovieDetailsId",
                table: "MovieCast");

            migrationBuilder.DropColumn(
                name: "MovieDetailsId",
                table: "Trailer");

            migrationBuilder.DropColumn(
                name: "MovieDetailsId",
                table: "MovieGenre");

            migrationBuilder.DropColumn(
                name: "MovieDetailsId",
                table: "MovieCast");

            migrationBuilder.RenameColumn(
                name: "Character",
                table: "MovieCast",
                newName: "character");
        }
    }
}
