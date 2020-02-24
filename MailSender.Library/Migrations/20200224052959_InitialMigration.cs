using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailSender.Library.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "EmailLists",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_EmailLists", x => x.Id); });

            migrationBuilder.CreateTable(
                "Emails",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    Subject = table.Column<string>(),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Emails", x => x.Id); });

            migrationBuilder.CreateTable(
                "Recipients",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50),
                    Address = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_Recipients", x => x.Id); });

            migrationBuilder.CreateTable(
                "Senders",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50),
                    Address = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_Senders", x => x.Id); });

            migrationBuilder.CreateTable(
                "Servers",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50),
                    Host = table.Column<string>(),
                    Port = table.Column<int>(),
                    EnableSsl = table.Column<bool>(),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Servers", x => x.Id); });

            migrationBuilder.CreateTable(
                "EmailListRecipients",
                table => new
                {
                    EmailListId = table.Column<int>(),
                    RecipientId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailListRecipients", x => new {x.EmailListId, x.RecipientId});
                    table.ForeignKey(
                        "FK_EmailListRecipients_EmailLists_EmailListId",
                        x => x.EmailListId,
                        "EmailLists",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_EmailListRecipients_Recipients_RecipientId",
                        x => x.RecipientId,
                        "Recipients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "SchedulerTasks",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<DateTime>(),
                    SenderId = table.Column<int>(),
                    RecipientsId = table.Column<int>(),
                    ServerId = table.Column<int>(),
                    EmailId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulerTasks", x => x.Id);
                    table.ForeignKey(
                        "FK_SchedulerTasks_Emails_EmailId",
                        x => x.EmailId,
                        "Emails",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_SchedulerTasks_EmailLists_RecipientsId",
                        x => x.RecipientsId,
                        "EmailLists",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_SchedulerTasks_Senders_SenderId",
                        x => x.SenderId,
                        "Senders",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_SchedulerTasks_Servers_ServerId",
                        x => x.ServerId,
                        "Servers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_EmailListRecipients_RecipientId",
                "EmailListRecipients",
                "RecipientId");

            migrationBuilder.CreateIndex(
                "IX_SchedulerTasks_EmailId",
                "SchedulerTasks",
                "EmailId");

            migrationBuilder.CreateIndex(
                "IX_SchedulerTasks_RecipientsId",
                "SchedulerTasks",
                "RecipientsId");

            migrationBuilder.CreateIndex(
                "IX_SchedulerTasks_SenderId",
                "SchedulerTasks",
                "SenderId");

            migrationBuilder.CreateIndex(
                "IX_SchedulerTasks_ServerId",
                "SchedulerTasks",
                "ServerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "EmailListRecipients");

            migrationBuilder.DropTable(
                "SchedulerTasks");

            migrationBuilder.DropTable(
                "Recipients");

            migrationBuilder.DropTable(
                "Emails");

            migrationBuilder.DropTable(
                "EmailLists");

            migrationBuilder.DropTable(
                "Senders");

            migrationBuilder.DropTable(
                "Servers");
        }
    }
}