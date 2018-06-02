using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReadingList.WriteModel.Migrations
{
    public partial class LastStatusUpdateDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadingTimeInTicks",
                table: "PrivateBookListItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStatusUpdateDate",
                table: "PrivateBookListItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ReadingTime",
                table: "PrivateBookListItems",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastStatusUpdateDate",
                table: "PrivateBookListItems");

            migrationBuilder.DropColumn(
                name: "ReadingTime",
                table: "PrivateBookListItems");

            migrationBuilder.AddColumn<long>(
                name: "ReadingTimeInTicks",
                table: "PrivateBookListItems",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
