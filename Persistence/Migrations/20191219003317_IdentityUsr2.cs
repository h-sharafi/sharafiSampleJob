using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class IdentityUsr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AspNetUsers_AppUserId1",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AppUserId1",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Activities");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AppUserId",
                table: "Activities",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AspNetUsers_AppUserId",
                table: "Activities",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AspNetUsers_AppUserId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AppUserId",
                table: "Activities");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Activities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AppUserId1",
                table: "Activities",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AspNetUsers_AppUserId1",
                table: "Activities",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
