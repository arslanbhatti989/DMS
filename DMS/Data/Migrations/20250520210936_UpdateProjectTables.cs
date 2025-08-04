using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Project_Sellers_Project_Seller_Id1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Project_Seller_Id1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Project_Seller_Id",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Project_Seller_Id1",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Project_Seller_Id",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Project_Seller_Id1",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Project_Seller_Id1",
                table: "Projects",
                column: "Project_Seller_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Project_Sellers_Project_Seller_Id1",
                table: "Projects",
                column: "Project_Seller_Id1",
                principalTable: "Project_Sellers",
                principalColumn: "Project_Seller_Id");
        }
    }
}
