using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Olimp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRegistrationAddEduOrgToParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.AddColumn<Guid>(
                name: "EduOrgId",
                table: "Participants",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Participants_EduOrgId",
                table: "Participants",
                column: "EduOrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_EduOrgs_EduOrgId",
                table: "Participants",
                column: "EduOrgId",
                principalTable: "EduOrgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_EduOrgs_EduOrgId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_EduOrgId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "EduOrgId",
                table: "Participants");

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EduOrgId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OlimpId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_EduOrgs_EduOrgId",
                        column: x => x.EduOrgId,
                        principalTable: "EduOrgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Olimps_OlimpId",
                        column: x => x.OlimpId,
                        principalTable: "Olimps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EduOrgId",
                table: "Registrations",
                column: "EduOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_OlimpId",
                table: "Registrations",
                column: "OlimpId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ParticipantId",
                table: "Registrations",
                column: "ParticipantId");
        }
    }
}
