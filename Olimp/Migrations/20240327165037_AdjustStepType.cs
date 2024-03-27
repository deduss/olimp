using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Olimp.Migrations
{
    /// <inheritdoc />
    public partial class AdjustStepType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EquationType",
                table: "Steps");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Steps",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Steps",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "EquationType",
                table: "Steps",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
