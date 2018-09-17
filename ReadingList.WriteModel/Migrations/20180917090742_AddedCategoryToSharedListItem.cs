using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReadingList.WriteModel.Migrations
{
    public partial class AddedCategoryToSharedListItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "SharedBookListItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedBookListItems_CategoryId",
                table: "SharedBookListItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedBookListItems_Categories_CategoryId",
                table: "SharedBookListItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedBookListItems_Categories_CategoryId",
                table: "SharedBookListItems");

            migrationBuilder.DropIndex(
                name: "IX_SharedBookListItems_CategoryId",
                table: "SharedBookListItems");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "SharedBookListItems");
        }
    }
}
