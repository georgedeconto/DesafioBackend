using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioBackend.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataCollectionPoints_IndicatorList_IndicatorId",
                table: "DataCollectionPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndicatorList",
                table: "IndicatorList");

            migrationBuilder.RenameTable(
                name: "IndicatorList",
                newName: "Indicators");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Indicators",
                table: "Indicators",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCollectionPoints_Indicators_IndicatorId",
                table: "DataCollectionPoints",
                column: "IndicatorId",
                principalTable: "Indicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataCollectionPoints_Indicators_IndicatorId",
                table: "DataCollectionPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Indicators",
                table: "Indicators");

            migrationBuilder.RenameTable(
                name: "Indicators",
                newName: "IndicatorList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndicatorList",
                table: "IndicatorList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCollectionPoints_IndicatorList_IndicatorId",
                table: "DataCollectionPoints",
                column: "IndicatorId",
                principalTable: "IndicatorList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
