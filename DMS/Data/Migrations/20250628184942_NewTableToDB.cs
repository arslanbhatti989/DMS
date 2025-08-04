using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewTableToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitBuyer",
                columns: table => new
                {
                    UnitBuyer_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unit_Id = table.Column<int>(type: "int", nullable: false),
                    BuyerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Person_Id = table.Column<int>(type: "int", nullable: true),
                    Company_Id = table.Column<int>(type: "int", nullable: true),
                    IsMainBuyer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitBuyer", x => x.UnitBuyer_Id);
                    table.ForeignKey(
                        name: "FK_UnitBuyer_Company_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "Company",
                        principalColumn: "Company_Id");
                    table.ForeignKey(
                        name: "FK_UnitBuyer_Persons_Person_Id",
                        column: x => x.Person_Id,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitBuyer_Units_Unit_Id",
                        column: x => x.Unit_Id,
                        principalTable: "Units",
                        principalColumn: "Unit_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitBuyer_Company_Id",
                table: "UnitBuyer",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UnitBuyer_Person_Id",
                table: "UnitBuyer",
                column: "Person_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UnitBuyer_Unit_Id",
                table: "UnitBuyer",
                column: "Unit_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitBuyer");
        }
    }
}
