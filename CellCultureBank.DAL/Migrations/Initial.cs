using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellCultureBank.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankFirsts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Movement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dewar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identifier = table.Column<int>(type: "int", nullable: false),
                    NameOfCellCulture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityOnLabel = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ActualBalance = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    CellLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfFreezing = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrozenByFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfDefrosting = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefrostedByFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clearing = table.Column<bool>(type: "bit", nullable: false),
                    Certification = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSeconds", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankFirsts");

            migrationBuilder.DropTable(
                name: "BankSeconds");
        }
    }
}
