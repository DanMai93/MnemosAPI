using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MnemosAPI.Migrations
{
    /// <inheritdoc />
    public partial class FiltriGet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EndCustomerId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EndCustomer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndCustomer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_EndCustomerId",
                table: "Projects",
                column: "EndCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_EndCustomer_EndCustomerId",
                table: "Projects",
                column: "EndCustomerId",
                principalTable: "EndCustomer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_EndCustomer_EndCustomerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "EndCustomer");

            migrationBuilder.DropIndex(
                name: "IX_Projects_EndCustomerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EndCustomerId",
                table: "Projects");
        }
    }
}
