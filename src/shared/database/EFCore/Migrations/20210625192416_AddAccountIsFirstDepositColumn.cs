using Microsoft.EntityFrameworkCore.Migrations;

namespace ourbank.src.shared.database.EFCore.Migrations
{
    public partial class AddAccountIsFirstDepositColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFirstDeposit",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "isFirstDeposit",
                table: "Accounts");
        }
    }
}
