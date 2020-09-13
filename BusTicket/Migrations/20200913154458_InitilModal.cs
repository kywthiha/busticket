using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTicket.Migrations
{
    public partial class InitilModal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusOperator",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(nullable: false),
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
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Traveller",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traveller", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "RouteDetail",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    RouteID = table.Column<int>(nullable: false),
                    CityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RouteDetail_City_CityID",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteDetail_Route_RouteID",
                        column: x => x.RouteID,
                        principalTable: "Route",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusSeat",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNo = table.Column<int>(nullable: false),
                    PositionX = table.Column<int>(nullable: false),
                    PositionY = table.Column<int>(nullable: false),
                    BusID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusSeat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BusSeat_Bus_BusID",
                        column: x => x.BusID,
                        principalTable: "Bus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(nullable: false),
                    DepatureTime = table.Column<DateTime>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    BusOperatorID = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    BusID = table.Column<int>(nullable: false),
                    RouteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schedule_Bus_BusID",
                        column: x => x.BusID,
                        principalTable: "Bus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_Route_RouteID",
                        column: x => x.RouteID,
                        principalTable: "Route",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    SeatedDate = table.Column<DateTime>(nullable: false),
                    ConfirmedDate = table.Column<DateTime>(nullable: false),
                    CanceledDate = table.Column<DateTime>(nullable: false),
                    TimeoutDate = table.Column<DateTime>(nullable: false),
                    TravellerID = table.Column<int>(nullable: false),
                    ScheduleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Booking_Schedule_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Traveller_TravellerID",
                        column: x => x.TravellerID,
                        principalTable: "Traveller",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDetail",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleID = table.Column<int>(nullable: false),
                    WeekDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ScheduleDetail_Schedule_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetail",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingID = table.Column<int>(nullable: false),
                    BusSeatID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingDetail_Booking_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetail_BusSeat_BusSeatID",
                        column: x => x.BusSeatID,
                        principalTable: "BusSeat",
                        principalColumn: "ID"/*,
                        onDelete: ReferentialAction.Cascade*/);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ScheduleID",
                table: "Booking",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_TravellerID",
                table: "Booking",
                column: "TravellerID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_BookingID",
                table: "BookingDetail",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_BusSeatID",
                table: "BookingDetail",
                column: "BusSeatID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusOperatorID",
                table: "Bus",
                column: "BusOperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_BusSeat_BusID",
                table: "BusSeat",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteDetail_CityID",
                table: "RouteDetail",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteDetail_RouteID",
                table: "RouteDetail",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_BusID",
                table: "Schedule",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_RouteID",
                table: "Schedule",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetail_ScheduleID",
                table: "ScheduleDetail",
                column: "ScheduleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetail");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "RouteDetail");

            migrationBuilder.DropTable(
                name: "ScheduleDetail");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "BusSeat");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Traveller");

            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "BusOperator");
        }
    }
}
