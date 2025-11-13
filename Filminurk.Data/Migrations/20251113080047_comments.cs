using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filminurk.Data.Migrations
{
    /// <inheritdoc />
    public partial class comments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actors",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    ActorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortraitID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Children = table.Column<int>(type: "int", nullable: true),
                    MovieID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.ActorID);
                    table.ForeignKey(
                        name: "FK_Actor_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserComments",
                columns: table => new
                {
                    CommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentScore = table.Column<int>(type: "int", nullable: false),
                    IsHelpful = table.Column<int>(type: "int", nullable: false),
                    IsHarmful = table.Column<int>(type: "int", nullable: false),
                    CommentCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommentDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MovieID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_UserComments_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actor_MovieID",
                table: "Actor",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_MovieID",
                table: "UserComments",
                column: "MovieID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "UserComments");

            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
