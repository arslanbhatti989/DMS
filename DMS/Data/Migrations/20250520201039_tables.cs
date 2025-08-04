using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Country_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status_Active = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Country_Id);
                });

            migrationBuilder.CreateTable(
                name: "Project_Sellers",
                columns: table => new
                {
                    Project_Seller_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seller_Company_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller_Company_Licence_Expiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Seller_Company_License_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller_Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Authorized_Signature_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Authorized_Signature_Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project_Sellers", x => x.Project_Seller_Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    City_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country_Id = table.Column<int>(type: "int", nullable: true),
                    City_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status_Active = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.City_Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Countries",
                        principalColumn: "Country_Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Project_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country_Id = table.Column<int>(type: "int", nullable: false),
                    City_Id = table.Column<int>(type: "int", nullable: false),
                    Project_Seller_Id = table.Column<int>(type: "int", nullable: false),
                    Project_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project_Used = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total_Floors = table.Column<int>(type: "int", nullable: false),
                    Total_Units = table.Column<int>(type: "int", nullable: false),
                    Project_Land_Area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Constructed_Area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Plot_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Saleable_Area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Construction_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completion_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Project_Id);
                    table.ForeignKey(
                        name: "FK_Projects_Cities_City_Id",
                        column: x => x.City_Id,
                        principalTable: "Cities",
                        principalColumn: "City_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Countries_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Countries",
                        principalColumn: "Country_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSellerAdminFee",
                columns: table => new
                {
                    Project_Seller_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_Id = table.Column<int>(type: "int", nullable: false),
                    OQood_Fee_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OQoob_Fee_Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Admin_Fee_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin_Fee_Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Other_Charges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rera_Fee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rera_Fee_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSellerAdminFee", x => x.Project_Seller_Id);
                    table.ForeignKey(
                        name: "FK_ProjectSellerAdminFee_Projects_Project_Id",
                        column: x => x.Project_Id,
                        principalTable: "Projects",
                        principalColumn: "Project_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Unit_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_Id = table.Column<int>(type: "int", nullable: false),
                    Floor_Number = table.Column<int>(type: "int", nullable: false),
                    Unit_Number = table.Column<int>(type: "int", nullable: false),
                    Interal_Unit_Size_Sqft = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    External_Unit_Size_Sqft = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total_Size_Sqft = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit_View = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Unit_Id);
                    table.ForeignKey(
                        name: "FK_Units_Projects_Project_Id",
                        column: x => x.Project_Id,
                        principalTable: "Projects",
                        principalColumn: "Project_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitTypes",
                columns: table => new
                {
                    Unity_Type_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unity_Type_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit_Type_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project_Id = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTypes", x => x.Unity_Type_Id);
                    table.ForeignKey(
                        name: "FK_UnitTypes_Projects_Project_Id",
                        column: x => x.Project_Id,
                        principalTable: "Projects",
                        principalColumn: "Project_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Country_Id",
                table: "Cities",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_City_Id",
                table: "Projects",
                column: "City_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Country_Id",
                table: "Projects",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSellerAdminFee_Project_Id",
                table: "ProjectSellerAdminFee",
                column: "Project_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Units_Project_Id",
                table: "Units",
                column: "Project_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTypes_Project_Id",
                table: "UnitTypes",
                column: "Project_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project_Sellers");

            migrationBuilder.DropTable(
                name: "ProjectSellerAdminFee");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "UnitTypes");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
