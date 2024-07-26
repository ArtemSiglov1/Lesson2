using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lesson2.Data.Migrations
{
    /// <inheritdoc />
    public partial class In : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ProfissionId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "User",
                type: "datetime2",
                nullable: false,
                computedColumnSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "[SecondName]+' '+[Name]");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleType);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleType", "Name" },
                values: new object[,]
                {
                    { 1, "Пользователь" },
                    { 2, "Гость" },
                    { 3, "Администратор" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfissionId",
                table: "User",
                column: "ProfissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Professions_ProfissionId",
                table: "User",
                column: "ProfissionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Professions_ProfissionId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ProfissionId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProfissionId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComputedColumnSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
