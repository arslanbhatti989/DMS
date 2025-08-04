using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectTablesNameUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Project_Seller_Id",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Project_Seller_Id",
                table: "Projects",
                column: "Project_Seller_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Project_Sellers_Project_Seller_Id",
                table: "Projects",
                column: "Project_Seller_Id",
                principalTable: "Project_Sellers",
                principalColumn: "Project_Seller_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Project_Sellers_Project_Seller_Id",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Project_Seller_Id",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Project_Seller_Id",
                table: "Projects");
        }
    }
}
