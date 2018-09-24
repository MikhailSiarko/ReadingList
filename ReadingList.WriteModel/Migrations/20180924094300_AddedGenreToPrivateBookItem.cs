using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReadingList.WriteModel.Migrations
{
    public partial class AddedGenreToPrivateBookItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GenreId",
                table: "PrivateBookListItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrivateBookListItems_GenreId",
                table: "PrivateBookListItems",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateBookListItems_Genres_GenreId",
                table: "PrivateBookListItems",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrivateBookListItems_Genres_GenreId",
                table: "PrivateBookListItems");

            migrationBuilder.DropIndex(
                name: "IX_PrivateBookListItems_GenreId",
                table: "PrivateBookListItems");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "PrivateBookListItems");
        }
    }
}
