using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReadingList.WriteModel.Migrations
{
    public partial class ReadingJournalRecordAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReadingJournalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemId = table.Column<int>(nullable: false),
                    StatusChangedDate = table.Column<DateTime>(nullable: false),
                    StatusSetTo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingJournalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingJournalRecords_PrivateBookListItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "PrivateBookListItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReadingJournalRecords_ItemId",
                table: "ReadingJournalRecords",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadingJournalRecords");
        }
    }
}
