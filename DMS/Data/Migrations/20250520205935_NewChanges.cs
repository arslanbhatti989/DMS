using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Project_Seller_Id",
                table: "ProjectSellerAdminFee",
                newName: "Project_Seller_Admin_Fee_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Project_Seller_Admin_Fee_Id",
                table: "ProjectSellerAdminFee",
                newName: "Project_Seller_Id");
        }
    }
}
