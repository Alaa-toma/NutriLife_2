using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrilife.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class planss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionistPlans_Users_NutritionistId",
                table: "NutritionistPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionistPlans",
                table: "NutritionistPlans");

            migrationBuilder.RenameTable(
                name: "NutritionistPlans",
                newName: "NutriPlans");

            migrationBuilder.RenameColumn(
                name: "NutritionistId",
                table: "NutriPlans",
                newName: "nutritionistId");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionistPlans_NutritionistId",
                table: "NutriPlans",
                newName: "IX_NutriPlans_nutritionistId");

            migrationBuilder.AlterColumn<string>(
                name: "nutritionistId",
                table: "NutriPlans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "nutritionistId",
                table: "NutritionistPlans",
                newName: "NutritionistId");

            migrationBuilder.RenameIndex(
                name: "IX_NutriPlans_nutritionistId",
                table: "NutritionistPlans",
                newName: "IX_NutritionistPlans_NutritionistId");

            migrationBuilder.AlterColumn<string>(
                name: "NutritionistId",
                table: "NutritionistPlans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionistPlans",
                table: "NutritionistPlans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionistPlans_Users_NutritionistId",
                table: "NutritionistPlans",
                column: "NutritionistId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
