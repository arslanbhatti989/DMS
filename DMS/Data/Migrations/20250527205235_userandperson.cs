using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class userandperson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Person_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Person_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalityCountry_Id = table.Column<int>(type: "int", nullable: true),
                    Passport_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passport_Expiry_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Passport_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emirates_Id_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emireate_Id_Expiry_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Alternate_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Line_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Line_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Country_Id = table.Column<int>(type: "int", nullable: true),
                    City_Id = table.Column<int>(type: "int", nullable: true),
                    Zip_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_Cities_City_Id",
                        column: x => x.City_Id,
                        principalTable: "Cities",
                        principalColumn: "City_Id");
                    table.ForeignKey(
                        name: "FK_Persons_Countries_Address_Country_Id",
                        column: x => x.Address_Country_Id,
                        principalTable: "Countries",
                        principalColumn: "Country_Id");
                    table.ForeignKey(
                        name: "FK_Persons_Countries_NationalityCountry_Id",
                        column: x => x.NationalityCountry_Id,
                        principalTable: "Countries",
                        principalColumn: "Country_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Address_Country_Id",
                table: "Persons",
                column: "Address_Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_City_Id",
                table: "Persons",
                column: "City_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_NationalityCountry_Id",
                table: "Persons",
                column: "NationalityCountry_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UserId",
                table: "Persons",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");
        }
    }
}
