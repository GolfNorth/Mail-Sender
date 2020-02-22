using Microsoft.EntityFrameworkCore.Migrations;

namespace MailSender.Library.Migrations
{
    public partial class EmailListEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EmailLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EmailLists");
        }
    }
}
