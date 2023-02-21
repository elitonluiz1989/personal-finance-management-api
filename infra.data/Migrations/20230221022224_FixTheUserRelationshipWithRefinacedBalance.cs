using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceManagement.Infra.Data.Migrations
{
    public partial class FixTheUserRelationshipWithRefinacedBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefinancedBalances_Users_UserId",
                table: "RefinancedBalances");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RefinancedBalances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AddForeignKey(
                name: "FK_RefinancedBalances_Users_UserId",
                table: "RefinancedBalances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefinancedBalances_Users_UserId",
                table: "RefinancedBalances");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RefinancedBalances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AddForeignKey(
                name: "FK_RefinancedBalances_Users_UserId",
                table: "RefinancedBalances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
