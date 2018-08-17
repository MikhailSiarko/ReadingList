using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReadingList.WriteModel.Migrations
{
    public partial class ChangedReadingTimeDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadingTime",
                table: "PrivateBookListItems");

            migrationBuilder.AddColumn<int>(
                name: "ReadingTimeInSeconds",
                table: "PrivateBookListItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadingTimeInSeconds",
                table: "PrivateBookListItems");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ReadingTime",
                table: "PrivateBookListItems",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
