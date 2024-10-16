using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTickets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Payments_PaymentId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Seances_SeanceId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_PaymentId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_SeanceId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "SeanceId",
                table: "Seats");

            migrationBuilder.CreateTable(
                name: "SeatAvailabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeatId = table.Column<int>(type: "integer", nullable: false),
                    SeanceId = table.Column<int>(type: "integer", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatAvailabilities_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SeatAvailabilities_Seances_SeanceId",
                        column: x => x.SeanceId,
                        principalTable: "Seances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatAvailabilities_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatAvailabilities_PaymentId",
                table: "SeatAvailabilities",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatAvailabilities_SeanceId",
                table: "SeatAvailabilities",
                column: "SeanceId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatAvailabilities_SeatId",
                table: "SeatAvailabilities",
                column: "SeatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatAvailabilities");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Seats",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Seats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SeanceId",
                table: "Seats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_PaymentId",
                table: "Seats",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeanceId",
                table: "Seats",
                column: "SeanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Payments_PaymentId",
                table: "Seats",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Seances_SeanceId",
                table: "Seats",
                column: "SeanceId",
                principalTable: "Seances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
