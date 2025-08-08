using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyDocumentAndDocumentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Update_At",
                table: "UnitTypes",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "Update_At",
                table: "Units",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "Update_At",
                table: "ProjectSellerAdminFee",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "Update_At",
                table: "Project_Sellers",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "Update_At",
                table: "Persons",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "Update_At",
                table: "Payment_Plans",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "Update_At",
                table: "Installments",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "Update_At",
                table: "Countries",
                newName: "Updated_At");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UnitTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Units",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "UnitBuyer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Created_By",
                table: "UnitBuyer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UnitBuyer",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "UnitBuyer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Updated_By",
                table: "UnitBuyer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectSellerAdminFee",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Project_Sellers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Persons",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Payment_Plans",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Installments",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Countries",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    DocumentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.DocumentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DocumentIssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_CompanyDocuments_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Company_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDocuments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDocuments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Project_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDocuments_CompanyId",
                table: "CompanyDocuments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDocuments_DocumentTypeId",
                table: "CompanyDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDocuments_ProjectId",
                table: "CompanyDocuments",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDocuments");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UnitTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "UnitBuyer");

            migrationBuilder.DropColumn(
                name: "Created_By",
                table: "UnitBuyer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UnitBuyer");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "UnitBuyer");

            migrationBuilder.DropColumn(
                name: "Updated_By",
                table: "UnitBuyer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectSellerAdminFee");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Project_Sellers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Payment_Plans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "UnitTypes",
                newName: "Update_At");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Units",
                newName: "Update_At");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "ProjectSellerAdminFee",
                newName: "Update_At");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Project_Sellers",
                newName: "Update_At");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Persons",
                newName: "Update_At");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Payment_Plans",
                newName: "Update_At");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Installments",
                newName: "Update_At");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Countries",
                newName: "Update_At");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Countries",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
