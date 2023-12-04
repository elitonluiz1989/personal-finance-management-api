using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceManagement.Infra.Data.Migrations
{
    public partial class CreateTransactionResidueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpadtedAt",
                table: "Transactions",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpadtedAt",
                table: "Balances",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPaid",
                table: "TransactionItems",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalAmount",
                table: "RefinancedBalances",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "RefinancedBalances",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "RefinancedBalances",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Installments",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "Financed",
                table: "Balances",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "Balances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Balances",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Balances",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Balances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<bool>(
                name: "Residue",
                table: "Balances",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.CreateTable(
                name: "TransactionResidue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionItemOriginId = table.Column<int>(type: "int", nullable: false),
                    TransactionItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionResidue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionResidue_TransactionItems_TransactionItemId",
                        column: x => x.TransactionItemId,
                        principalTable: "TransactionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionResidue_TransactionItems_TransactionItemOriginId",
                        column: x => x.TransactionItemOriginId,
                        principalTable: "TransactionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionResidue_TransactionItemId",
                table: "TransactionResidue",
                column: "TransactionItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionResidue_TransactionItemOriginId",
                table: "TransactionResidue",
                column: "TransactionItemOriginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionResidue");

            migrationBuilder.DropColumn(
                name: "Residue",
                table: "Balances");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Transactions",
                newName: "UpadtedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Balances",
                newName: "UpadtedAt");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPaid",
                table: "TransactionItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalAmount",
                table: "RefinancedBalances",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "RefinancedBalances",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "RefinancedBalances",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Installments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<bool>(
                name: "Financed",
                table: "Balances",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "Balances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Balances",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Balances",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpadtedAt",
                table: "Balances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);
        }
    }
}
