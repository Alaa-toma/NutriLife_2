using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrilife.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class plans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserPlanId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserPlanId",
                table: "Subscriptions",
                column: "UserPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_NutritionistPlans_UserPlanId",
                table: "Subscriptions",
                column: "UserPlanId",
                principalTable: "NutritionistPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_NutritionistPlans_UserPlanId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_UserPlanId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "UserPlanId",
                table: "Subscriptions");
        }
    }
}
