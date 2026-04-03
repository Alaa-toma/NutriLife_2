using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrilife.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class appointmentedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutriworkingTime");

            migrationBuilder.DropColumn(
                name: "appointment_date",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Appointments",
                newName: "Status");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "Appointments",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<DateOnly>(
                name: "date",
                table: "Appointments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "date",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Appointments",
                newName: "status");

            migrationBuilder.AddColumn<DateTime>(
                name: "appointment_date",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "NutriworkingTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nutritionistId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutriworkingTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutriworkingTime_Users_nutritionistId",
                        column: x => x.nutritionistId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NutriworkingTime_nutritionistId",
                table: "NutriworkingTime",
                column: "nutritionistId");
        }
    }
}
