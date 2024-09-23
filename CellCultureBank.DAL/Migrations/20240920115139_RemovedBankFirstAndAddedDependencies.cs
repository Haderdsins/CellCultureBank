using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellCultureBank.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBankFirstAndAddedDependencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankFirsts");

            migrationBuilder.DropTable(
                name: "BankSeconds");

            migrationBuilder.CreateTable(
                name: "BankOfCells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfFreezing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefrostedByUserId = table.Column<int>(type: "int", nullable: true),
                    DateOfDefrosting = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FrozenByUserId = table.Column<int>(type: "int", nullable: true),
                    Clearing = table.Column<bool>(type: "bit", nullable: false),
                    Certification = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankOfCells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankOfCells_Users_DefrostedByUserId",
                        column: x => x.DefrostedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankOfCells_Users_FrozenByUserId",
                        column: x => x.FrozenByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankOfCells_DefrostedByUserId",
                table: "BankOfCells",
                column: "DefrostedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankOfCells_FrozenByUserId",
                table: "BankOfCells",
                column: "FrozenByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankOfCells");

            migrationBuilder.CreateTable(
                name: "BankFirsts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActualBalance = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dewar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identifier = table.Column<int>(type: "int", nullable: false),
                    Movement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOfCellCulture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityOnLabel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankFirsts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankSeconds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certification = table.Column<bool>(type: "bit", nullable: false),
                    Clearing = table.Column<bool>(type: "bit", nullable: false),
                    DateOfDefrosting = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfFreezing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefrostedByFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrozenByFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSeconds", x => x.Id);
                });
        }
    }
}
