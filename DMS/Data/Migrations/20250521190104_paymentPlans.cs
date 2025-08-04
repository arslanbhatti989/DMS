using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class paymentPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment_Plans",
                columns: table => new
                {
                    Payment_Plan_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_Id = table.Column<int>(type: "int", nullable: true),
                    Plan_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plan_Status = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment_Plans", x => x.Payment_Plan_Id);
                    table.ForeignKey(
                        name: "FK_Payment_Plans_Projects_Project_Id",
                        column: x => x.Project_Id,
                        principalTable: "Projects",
                        principalColumn: "Project_Id");
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    Installment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Payment_Plan_Id = table.Column<int>(type: "int", nullable: true),
                    Installment_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sequence_Number = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.Installment_Id);
                    table.ForeignKey(
                        name: "FK_Installments_Payment_Plans_Payment_Plan_Id",
                        column: x => x.Payment_Plan_Id,
                        principalTable: "Payment_Plans",
                        principalColumn: "Payment_Plan_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installments_Payment_Plan_Id",
                table: "Installments",
                column: "Payment_Plan_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Plans_Project_Id",
                table: "Payment_Plans",
                column: "Project_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "Payment_Plans");
        }
    }
}
