using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    BoardWidth = table.Column<int>(type: "INTEGER", nullable: false),
                    BoardHeight = table.Column<int>(type: "INTEGER", nullable: false),
                    GridWidth = table.Column<int>(type: "INTEGER", nullable: false),
                    GridHeight = table.Column<int>(type: "INTEGER", nullable: false),
                    WinCondition = table.Column<int>(type: "INTEGER", nullable: false),
                    MovePieceAfterNMoves = table.Column<int>(type: "INTEGER", nullable: false),
                    Player1PieceAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    Player2PieceAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1280, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaveGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    GameBoard = table.Column<string>(type: "TEXT", maxLength: 10240, nullable: false),
                    GamePassword = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    NextMoveBy = table.Column<int>(type: "INTEGER", nullable: false),
                    Player1PieceAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    Player2PieceAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    GridHeight = table.Column<int>(type: "INTEGER", nullable: false),
                    GridWidth = table.Column<int>(type: "INTEGER", nullable: false),
                    GridCenterX = table.Column<int>(type: "INTEGER", nullable: false),
                    GridCenterY = table.Column<int>(type: "INTEGER", nullable: false),
                    StartRow = table.Column<int>(type: "INTEGER", nullable: false),
                    EndRow = table.Column<int>(type: "INTEGER", nullable: false),
                    StartColumn = table.Column<int>(type: "INTEGER", nullable: false),
                    EndColumn = table.Column<int>(type: "INTEGER", nullable: false),
                    MadeMoves = table.Column<int>(type: "INTEGER", nullable: false),
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    GameTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaveGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaveGames_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaveGames_ConfigurationId",
                table: "SaveGames",
                column: "ConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameType");

            migrationBuilder.DropTable(
                name: "SaveGames");

            migrationBuilder.DropTable(
                name: "Configurations");
        }
    }
}
