using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReadingList.WriteModel.Migrations
{
    public partial class RemovedHsonFieldsPropertyInBookList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonFields",
                table: "BookLists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JsonFields",
                table: "BookLists",
                nullable: true);
        }
    }
}
