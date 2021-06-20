using Microsoft.EntityFrameworkCore.Migrations;

namespace ourbank.src.shared.database.EFCore.Migrations
{
    public partial class RemoveAccountUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
