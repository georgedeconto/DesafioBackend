using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioBackend.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ResultType",
                table: "IndicatorList",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "nvarchar(24)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ResultType",
                table: "IndicatorList",
                type: "nvarchar(24)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
