using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Web.Data.Migrations
{
    public partial class AddForeinKeyPropertiesToGig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Venue",
                table: "Gigs");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Gigs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Gigs");

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "Gigs",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
