using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace microwave_benner.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_heating_program : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "heatingProgramId",
                table: "heatingTasks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "heatingPrograms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    food = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    time = table.Column<int>(type: "integer", nullable: false),
                    power = table.Column<int>(type: "integer", nullable: false),
                    heatingChar = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false),
                    instructions = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    custom = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_heatingPrograms", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_heatingTasks_heatingProgramId",
                table: "heatingTasks",
                column: "heatingProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_heatingTasks_heatingPrograms_heatingProgramId",
                table: "heatingTasks",
                column: "heatingProgramId",
                principalTable: "heatingPrograms",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_heatingTasks_heatingPrograms_heatingProgramId",
                table: "heatingTasks");

            migrationBuilder.DropTable(
                name: "heatingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_heatingTasks_heatingProgramId",
                table: "heatingTasks");

            migrationBuilder.DropColumn(
                name: "heatingProgramId",
                table: "heatingTasks");
        }
    }
}
