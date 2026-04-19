using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrilife.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class nullableplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_planOfDays_mealPlanId_DayNumber",
                table: "planOfDays");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanOfDayId",
                table: "scheduledMeals",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "mealPlanId",
                table: "planOfDays",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_planOfDays_mealPlanId_DayNumber",
                table: "planOfDays",
                columns: new[] { "mealPlanId", "DayNumber" },
                unique: true,
                filter: "[mealPlanId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_planOfDays_mealPlanId_DayNumber",
                table: "planOfDays");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanOfDayId",
                table: "scheduledMeals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "mealPlanId",
                table: "planOfDays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_planOfDays_mealPlanId_DayNumber",
                table: "planOfDays",
                columns: new[] { "mealPlanId", "DayNumber" },
                unique: true);
        }
    }
}
