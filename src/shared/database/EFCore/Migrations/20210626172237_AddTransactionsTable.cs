using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ourbank.src.shared.database.EFCore.Migrations
{
    public partial class AddTransactionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    sender_id = table.Column<string>(type: "text", nullable: false),
                    recipient_id = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_senderId",
                table: "Transactions",
                column: "sender_id");
                

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_recipientId",
                table: "Transactions",
                column: "recipient_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_senderId",
                table: "Transactions",
                column: "sender_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_recipientId",
                table: "Transactions",
                column: "recipient_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

          migrationBuilder.DropForeignKey(
              name: "FK_Transactions_Users_recipientId",
              table: "Transactions"
            );

            migrationBuilder.DropForeignKey(
              name: "FK_Transactions_Users_senderId",
              table: "Transactions"
            );

            migrationBuilder.DropIndex(
              name: "IX_Transactions_recipientId",
              table: "Transactions"
            );

            migrationBuilder.DropIndex(
              name: "IX_Transactions_senderId",
              table: "Transactions"
            );

            migrationBuilder.DropTable(
                name: "Transactions");
            
        }
    }
}
