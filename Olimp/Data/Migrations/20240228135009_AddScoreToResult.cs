using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Olimp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreToResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "Results",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Results");
        }
    }
}
