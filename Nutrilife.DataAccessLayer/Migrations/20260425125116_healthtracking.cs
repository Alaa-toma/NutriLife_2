using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrilife.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class healthtracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "healthTrackings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriptioId = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_healthTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_healthTrackings_Subscriptions_SubscriptioId",
                        column: x => x.SubscriptioId,
                        principalTable: "Subscriptions",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_healthTrackings_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inBodyScans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthTrackingId = table.Column<int>(type: "int", nullable: false),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowAIOutput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weight = table.Column<float>(type: "real", nullable: false),
                    BMI = table.Column<float>(type: "real", nullable: false),
                    BodyFatPercentage = table.Column<float>(type: "real", nullable: false),
                    MuscleMass = table.Column<float>(type: "real", nullable: false),
                    FatMass = table.Column<float>(type: "real", nullable: false),
                    VisceralFatLevel = table.Column<float>(type: "real", nullable: false),
                    TotalBodyWater = table.Column<float>(type: "real", nullable: false),
                    BasalMetabolicRate = table.Column<float>(type: "real", nullable: false),
                    BoneMineralContent = table.Column<float>(type: "real", nullable: false),
                    ScannedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inBodyScans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inBodyScans_healthTrackings_HealthTrackingId",
                        column: x => x.HealthTrackingId,
                        principalTable: "healthTrackings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "manualMeasurements",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthTrackingId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: true),
                    WaistCircumference = table.Column<float>(type: "real", nullable: true),
                    HipCircumference = table.Column<float>(type: "real", nullable: true),
                    ChestCircumference = table.Column<float>(type: "real", nullable: true),
                    ArmCircumference = table.Column<float>(type: "real", nullable: true),
                    ThighCircumference = table.Column<float>(type: "real", nullable: true),
                    MeasuredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manualMeasurements", x => x.id);
                    table.ForeignKey(
                        name: "FK_manualMeasurements_healthTrackings_HealthTrackingId",
                        column: x => x.HealthTrackingId,
                        principalTable: "healthTrackings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_healthTrackings_ClientId",
                table: "healthTrackings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_healthTrackings_SubscriptioId",
                table: "healthTrackings",
                column: "SubscriptioId");

            migrationBuilder.CreateIndex(
                name: "IX_inBodyScans_HealthTrackingId",
                table: "inBodyScans",
                column: "HealthTrackingId");

            migrationBuilder.CreateIndex(
                name: "IX_manualMeasurements_HealthTrackingId",
                table: "manualMeasurements",
                column: "HealthTrackingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inBodyScans");

            migrationBuilder.DropTable(
                name: "manualMeasurements");

            migrationBuilder.DropTable(
                name: "healthTrackings");
        }
    }
}
