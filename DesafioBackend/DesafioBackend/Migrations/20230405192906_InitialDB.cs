using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndicatorList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ResultType = table.Column<int>(type: "nvarchar(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataCollectionPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IndicatorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataCollectionPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataCollectionPoints_IndicatorList_IndicatorId",
                        column: x => x.IndicatorId,
                        principalTable: "IndicatorList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataCollectionPoints_Date",
                table: "DataCollectionPoints",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_DataCollectionPoints_IndicatorId",
                table: "DataCollectionPoints",
                column: "IndicatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataCollectionPoints");

            migrationBuilder.DropTable(
                name: "IndicatorList");
        }
    }
}
