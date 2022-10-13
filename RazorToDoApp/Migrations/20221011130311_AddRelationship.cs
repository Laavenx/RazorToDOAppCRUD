using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorToDoApp.Migrations
{
    public partial class AddRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDoLists");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ToDoLists",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DbUserId",
                table: "ToDoLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_DbUserId",
                table: "ToDoLists",
                column: "DbUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoLists_User_DbUserId",
                table: "ToDoLists",
                column: "DbUserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoLists_User_DbUserId",
                table: "ToDoLists");

            migrationBuilder.DropIndex(
                name: "IX_ToDoLists_DbUserId",
                table: "ToDoLists");

            migrationBuilder.DropColumn(
                name: "DbUserId",
                table: "ToDoLists");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ToDoLists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ToDoLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
