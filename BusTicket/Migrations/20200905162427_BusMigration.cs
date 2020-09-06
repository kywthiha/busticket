using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTicket.Migrations
{
    public partial class BusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusOperator",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Phoneno = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusOperator", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoOfSeat = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    BusOperatorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bus_BusOperator_BusOperatorID",
                        column: x => x.BusOperatorID,
                        principalTable: "BusOperator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusOperatorID",
                table: "Bus",
                column: "BusOperatorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "BusOperator");
        }
    }
}
