using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrilife.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class aaw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutriPlans_Users_nutritionistId",
                table: "NutriPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutriPlans",
                table: "NutriPlans");

            migrationBuilder.RenameTable(
                name: "NutriPlans",
                newName: "NutritionistPlans");

            migrationBuilder.RenameIndex(
                name: "IX_NutriPlans_nutritionistId",
                table: "NutritionistPlans",
                newName: "IX_NutritionistPlans_nutritionistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionistPlans",
                table: "NutritionistPlans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionistPlans_Users_nutritionistId",
                table: "NutritionistPlans",
                column: "nutritionistId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionistPlans_Users_nutritionistId",
                table: "NutritionistPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionistPlans",
                table: "NutritionistPlans");

            migrationBuilder.RenameTable(
                name: "NutritionistPlans",
                newName: "NutriPlans");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionistPlans_nutritionistId",
                table: "NutriPlans",
                newName: "IX_NutriPlans_nutritionistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutriPlans",
                table: "NutriPlans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NutriPlans_Users_nutritionistId",
                table: "NutriPlans",
                column: "nutritionistId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
