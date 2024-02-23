using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Olimp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRoleFromParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Participants");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Participants",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Participants",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Role",
                table: "Participants",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
