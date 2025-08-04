using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class testingDataRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestModalsMain_TestModals_TestModalId",
                table: "TestModalsMain");

            migrationBuilder.DropIndex(
                name: "IX_TestModalsMain_TestModalId",
                table: "TestModalsMain");

            migrationBuilder.DropColumn(
                name: "TestModalId",
                table: "TestModalsMain");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestModalsMain_TestModals_TestModelId",
                table: "TestModalsMain");

            migrationBuilder.DropIndex(
                name: "IX_TestModalsMain_TestModelId",
                table: "TestModalsMain");

            migrationBuilder.AddColumn<int>(
                name: "TestModalId",
                table: "TestModalsMain",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestModalsMain_TestModalId",
                table: "TestModalsMain",
                column: "TestModalId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestModalsMain_TestModals_TestModalId",
                table: "TestModalsMain",
                column: "TestModalId",
                principalTable: "TestModals",
                principalColumn: "Id");
        }
    }
}
