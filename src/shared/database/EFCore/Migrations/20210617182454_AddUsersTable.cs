using Microsoft.EntityFrameworkCore.Migrations;

namespace ourbank.src.shared.database.EFCore.Migrations {
  public partial class AddUsersTable : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.CreateTable(
        name: "Users",
        columns: table => new {
          id = table.Column<string>(type: "text", nullable: false),
          name = table.Column<string>(type: "text", nullable: false),
          email = table.Column<string>(type: "text", nullable: false),
          password = table.Column<string>(type: "text", nullable: false),
          avatar_url = table.Column<string>(type: "text", nullable: true),
          account_id = table.Column<string>(type: "text", nullable: true)
          
        },
        constraints: table => {
          table.PrimaryKey("PK_Users", x => x.id);
        }
      );
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(name: "Users");
    }
  }
}
