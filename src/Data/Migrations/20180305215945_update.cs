using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PhotoExhibiter.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exhibits_AspNetUsers_PhotographerId",
                table: "Exhibits");

            migrationBuilder.AddForeignKey(
                name: "FK_Exhibits_AspNetUsers_PhotographerId",
                table: "Exhibits",
                column: "PhotographerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exhibits_AspNetUsers_PhotographerId",
                table: "Exhibits");

            migrationBuilder.AddForeignKey(
                name: "FK_Exhibits_AspNetUsers_PhotographerId",
                table: "Exhibits",
                column: "PhotographerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
