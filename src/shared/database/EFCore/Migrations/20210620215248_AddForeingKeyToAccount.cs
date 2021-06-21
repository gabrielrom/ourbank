using Microsoft.EntityFrameworkCore.Migrations;

namespace ourbank.src.shared.database.EFCore.Migrations
{
    public partial class AddForeingKeyToAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_account_id",
                table: "Users",
                column: "account_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Accounts_account_id",
                table: "Users",
                column: "account_id",
                principalTable: "Accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Accounts_account_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_account_id",
                table: "Users");
        }
    }
}
