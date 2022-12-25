using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lyubishchev.Migrations
{
    public partial class Added_TimePeriodCategory_To_TimePeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "AppTimePeriod",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_AppTimePeriod_CategoryId",
                table: "AppTimePeriod",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTimePeriod_AppTimePeriodCategory_CategoryId",
                table: "AppTimePeriod",
                column: "CategoryId",
                principalTable: "AppTimePeriodCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTimePeriod_AppTimePeriodCategory_CategoryId",
                table: "AppTimePeriod");

            migrationBuilder.DropIndex(
                name: "IX_AppTimePeriod_CategoryId",
                table: "AppTimePeriod");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "AppTimePeriod");
        }
    }
}
