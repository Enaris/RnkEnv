using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnvRnk.DataAccess.Migrations
{
    public partial class articleauthoridadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_RnkUsers_AuthorId",
                table: "Articles");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_RnkUsers_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "RnkUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_RnkUsers_AuthorId",
                table: "Articles");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_RnkUsers_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "RnkUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
