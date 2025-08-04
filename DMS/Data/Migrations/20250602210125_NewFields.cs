using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Employer",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Funds",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marital",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ownership",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PassportCountry_Id",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PassportNationality_Id",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PassportCountry_Id",
                table: "Persons",
                column: "PassportCountry_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PassportNationality_Id",
                table: "Persons",
                column: "PassportNationality_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Countries_PassportCountry_Id",
                table: "Persons",
                column: "PassportCountry_Id",
                principalTable: "Countries",
                principalColumn: "Country_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Countries_PassportNationality_Id",
                table: "Persons",
                column: "PassportNationality_Id",
                principalTable: "Countries",
                principalColumn: "Country_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Countries_PassportCountry_Id",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Countries_PassportNationality_Id",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_PassportCountry_Id",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_PassportNationality_Id",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Employer",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Funds",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Marital",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Ownership",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PassportCountry_Id",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PassportNationality_Id",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "AspNetUsers");
        }
    }
}
