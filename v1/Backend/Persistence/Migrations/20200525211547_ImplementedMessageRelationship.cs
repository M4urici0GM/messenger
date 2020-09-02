using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ImplementedMessageRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(nullable: false),
                    IsSeen = table.Column<bool>(nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    SeenDate = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MessageId = table.Column<Guid>(nullable: false),
                    UserToId = table.Column<Guid>(nullable: false),
                    UserFromId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMessages_Users_UserFromId",
                        column: x => x.UserFromId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMessages_Users_UserToId",
                        column: x => x.UserToId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_MessageId",
                table: "UserMessages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_UserFromId",
                table: "UserMessages",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_UserToId",
                table: "UserMessages",
                column: "UserToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");
        }
    }
}
