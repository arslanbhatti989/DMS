using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Unit_Id",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Commission",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Company_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trade_License_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    License_Expiry_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country_Id = table.Column<int>(type: "int", nullable: true),
                    City_Id = table.Column<int>(type: "int", nullable: true),
                    Address_Line_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Line_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Person_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Person_Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Person_Passport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Person_Emirates_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Person_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Person_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit_Id = table.Column<int>(type: "int", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Company_Id);
                    table.ForeignKey(
                        name: "FK_Company_Cities_City_Id",
                        column: x => x.City_Id,
                        principalTable: "Cities",
                        principalColumn: "City_Id");
                    table.ForeignKey(
                        name: "FK_Company_Countries_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Countries",
                        principalColumn: "Country_Id");
                    table.ForeignKey(
                        name: "FK_Company_Units_Unit_Id",
                        column: x => x.Unit_Id,
                        principalTable: "Units",
                        principalColumn: "Unit_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Unit_Id",
                table: "Persons",
                column: "Unit_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_City_Id",
                table: "Company",
                column: "City_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Country_Id",
                table: "Company",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Unit_Id",
                table: "Company",
                column: "Unit_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Units_Unit_Id",
                table: "Persons",
                column: "Unit_Id",
                principalTable: "Units",
                principalColumn: "Unit_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Units_Unit_Id",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Persons_Unit_Id",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Unit_Id",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "AspNetUsers");
        }
    }
}
