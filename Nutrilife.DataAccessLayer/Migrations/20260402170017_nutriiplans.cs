using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrilife.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class nutriiplans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userPlaneId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_userPlaneId",
                table: "Subscriptions",
                column: "userPlaneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_NutritionistPlans_userPlaneId",
                table: "Subscriptions",
                column: "userPlaneId",
                principalTable: "NutritionistPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_NutritionistPlans_userPlaneId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_userPlaneId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "userPlaneId",
                table: "Subscriptions");
        }
    }
}
