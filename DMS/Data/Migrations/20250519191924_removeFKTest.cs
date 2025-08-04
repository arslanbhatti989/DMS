using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeFKTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestModalsMain_TestModals_TestModelId",
                table: "TestModalsMain");

            migrationBuilder.DropIndex(
                name: "IX_TestModalsMain_TestModelId",
                table: "TestModalsMain");

            migrationBuilder.DropColumn(
                name: "TestModelId",
                table: "TestModalsMain");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestModelId",
                table: "TestModalsMain",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestModalsMain_TestModelId",
                table: "TestModalsMain",
                column: "TestModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestModalsMain_TestModals_TestModelId",
                table: "TestModalsMain",
                column: "TestModelId",
                principalTable: "TestModals",
                principalColumn: "Id");
        }
    }
}
