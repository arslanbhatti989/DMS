using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class testmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestModals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestModals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestModalsMain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestModalId = table.Column<int>(type: "int", nullable: true),
                    TestModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestModalsMain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestModalsMain_TestModals_TestModalId",
                        column: x => x.TestModalId,
                        principalTable: "TestModals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestModalsMain_TestModalId",
                table: "TestModalsMain",
                column: "TestModalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestModalsMain");

            migrationBuilder.DropTable(
                name: "TestModals");
        }
    }
}
