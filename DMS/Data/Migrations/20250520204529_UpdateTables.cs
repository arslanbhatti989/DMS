using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Cities_City_Id",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Countries_Country_Id",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSellerAdminFee_Projects_Project_Id",
                table: "ProjectSellerAdminFee");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Projects_Project_Id",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "Project_Id",
                table: "Units",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Unit_Type_Id",
                table: "Units",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Project_Id",
                table: "ProjectSellerAdminFee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Project_Seller_Id",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Country_Id",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "City_Id",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Units_Unit_Type_Id",
                table: "Units",
                column: "Unit_Type_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Cities_City_Id",
                table: "Projects",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "City_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Countries_Country_Id",
                table: "Projects",
                column: "Country_Id",
                principalTable: "Countries",
                principalColumn: "Country_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSellerAdminFee_Projects_Project_Id",
                table: "ProjectSellerAdminFee",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Project_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Projects_Project_Id",
                table: "Units",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Project_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_UnitTypes_Unit_Type_Id",
                table: "Units",
                column: "Unit_Type_Id",
                principalTable: "UnitTypes",
                principalColumn: "Unity_Type_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Cities_City_Id",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Countries_Country_Id",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSellerAdminFee_Projects_Project_Id",
                table: "ProjectSellerAdminFee");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Projects_Project_Id",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_UnitTypes_Unit_Type_Id",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_Unit_Type_Id",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Unit_Type_Id",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "Project_Id",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Units",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Project_Id",
                table: "ProjectSellerAdminFee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Project_Seller_Id",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Country_Id",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "City_Id",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Cities_City_Id",
                table: "Projects",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "City_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Countries_Country_Id",
                table: "Projects",
                column: "Country_Id",
                principalTable: "Countries",
                principalColumn: "Country_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSellerAdminFee_Projects_Project_Id",
                table: "ProjectSellerAdminFee",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Project_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Projects_Project_Id",
                table: "Units",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Project_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
