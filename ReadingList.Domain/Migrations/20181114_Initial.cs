using System.Data;
using FluentMigrator;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Migrations
{
    [Migration(20181114114000)]
    public class Initial : Migration
    {
        public override void Up()
        {
            Create.Table("Genres")
                .WithColumn("Id").AsFixedLengthString(100).Indexed("IX_Genres_Id").PrimaryKey("PK_Genres").NotNullable()
                .WithColumn("Name").AsFixedLengthString(100).Nullable()
                .WithColumn("ParentId").AsFixedLengthString(100).Indexed("IX_Genres_ParentId")
                    .ForeignKey("FK_Genres_Genres_ParentId", "Genres", "Id")
                    .OnDelete(Rule.SetNull).Nullable();

            Create.Table("Profiles")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_Profiles").Identity().NotNullable()
                .WithColumn("Email").AsFixedLengthString(50).Unique().Nullable()
                .WithColumn("Firstname").AsFixedLengthString(50).Nullable()
                .WithColumn("Lastname").AsFixedLengthString(50).Nullable();

            Create.Table("Roles")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_Roles").Identity().NotNullable()
                .WithColumn("Name").AsFixedLengthString(30).Unique().Nullable();

            Create.Table("Tags")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_Tags").Identity().NotNullable()
                .WithColumn("Name").AsFixedLengthString(30).Unique().Nullable();

            Create.Table("Books")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_Books").Identity().NotNullable()
                .WithColumn("Author").AsString().NotNullable()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("GenreId").AsString().Indexed("IX_Books_GenreId")
                    .ForeignKey("FK_Books_Genres_GenreId", "Genres", "Id")
                    .OnDelete(Rule.SetNull).Nullable();

            Create.UniqueConstraint("IX_Books_Title_Author").OnTable("Books").Columns("Title", "Author");

            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_Users").Identity().NotNullable()
                .WithColumn("Login").AsFixedLengthString(50).Unique().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("ProfileId").AsInt32().Indexed("IX_Users_ProfileId")
                    .ForeignKey("FK_Users_Profiles_ProfileId", "Profiles", "Id")
                    .OnDelete(Rule.Cascade).NotNullable()
                .WithColumn("RoleId").AsInt32().Indexed("IX_Users_RoleId")
                    .ForeignKey("FK_Users_Roles_RoleId", "Roles", "Id")
                    .OnDelete(Rule.Cascade).NotNullable();

            Create.Table("BookTags")
                .WithColumn("TagId").AsInt32().ForeignKey("FK_BookTags_Tags_TagId", "Tags", "Id").OnDelete(Rule.Cascade)
                .NotNullable()
                .WithColumn("BookId").AsInt32().Indexed("IX_BookTags_BookId")
                    .ForeignKey("FK_BookTags_Books_BookId", "Books", "Id")
                    .OnDelete(Rule.Cascade).NotNullable();

            Create.PrimaryKey("PK_BookTags").OnTable("BookTags").Columns("TagId", "BookId");

            Create.Table("BookLists")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_BookLists").Identity().NotNullable()
                .WithColumn("Name").AsFixedLengthString(50).NotNullable()
                .WithColumn("OwnerId").AsInt32().Indexed("IX_BookLists_OwnerId")
                    .ForeignKey("FK_BookLists_Users_OwnerId", "Users", "Id")
                    .OnDelete(Rule.Cascade).NotNullable()
                .WithColumn("Type").AsInt32().NotNullable();

            Create.Table("BookListModerators")
                .WithColumn("UserId").AsInt32().ForeignKey("FK_BookListModerators_Users_UserId", "Users", "Id")
                    .OnDelete(Rule.Cascade).NotNullable()
                .WithColumn("BookListId").AsInt32().Indexed("IX_BookListModerators_BookListId")
                    .ForeignKey("FK_BookListModerators_BookLists_BookListId", "BookLists", "Id").OnDelete(Rule.Cascade)
                    .NotNullable();

            Create.PrimaryKey("PK_BookListModerators").OnTable("BookListModerators").Columns("UserId", "BookListId");

            Create.Table("PrivateBookListItems")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_PrivateBookListItems").Identity().NotNullable()
                .WithColumn("BookId").AsInt32().ForeignKey("FK_PrivateBookListItems_Books_BookId", "Books", "Id")
                    .OnDelete(Rule.Cascade).NotNullable()
                .WithColumn("LastStatusUpdateDate").AsString().NotNullable()
                .WithColumn("ReadingTimeInSeconds").AsInt32().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("BookListId").AsInt32().Indexed("IX_PrivateBookListItems_BookListId")
                .ForeignKey("FK_PrivateBookListItems_BookLists_BookListId", "BookLists", "Id").OnDelete(Rule.Cascade)
                    .NotNullable();

            Create.Table("SharedBookListItems")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_SharedBookListItems").Identity().NotNullable()
                .WithColumn("BookId").AsInt32().ForeignKey("FK_SharedBookListItems_Books_BookId", "Books", "Id")
                    .OnDelete(Rule.Cascade).NotNullable()
                .WithColumn("BookListId").AsInt32().Indexed("IX_SharedBookListItems_BookListId")
                    .ForeignKey("FK_SharedBookListItems_BookLists_BookListId", "BookLists", "Id").OnDelete(Rule.Cascade)
                .NotNullable();

            Create.Table("SharedBookListTags")
                .WithColumn("SharedBookListId").AsInt32()
                    .ForeignKey("FK_SharedBookListTags_BookLists_SharedBookListId", "BookLists", "Id")
                    .OnDelete(Rule.Cascade).NotNullable()
                .WithColumn("TagId").AsInt32().Indexed("IX_SharedBookListTags_TagId")
                    .ForeignKey("FK_SharedBookListTags_Tags_TagId", "Tags", "Id")
                    .OnDelete(Rule.Cascade).NotNullable();

            Create.PrimaryKey("PK_SharedBookListTags").OnTable("SharedBookListTags")
                .Columns("SharedBookListId", "TagId");

            foreach (var genre in SeedData.Genres())
            {
                Insert.IntoTable("Genres").Row(new {genre.Id, genre.Name, genre.ParentId});
            }

            foreach (var role in SeedData.Roles())
            {
                Insert.IntoTable("Roles").Row(role);
            }
        }

        public override void Down()
        {
            Delete.Table("Genres");

            Delete.Table("Profiles");

            Delete.Table("Roles");

            Delete.Table("Tags");

            Delete.Table("Books");

            Delete.Table("Users");

            Delete.Table("BookTags");

            Delete.Table("BookLists");

            Delete.Table("BookListModerators");

            Delete.Table("PrivateBookListItems");

            Delete.Table("SharedBookListItems");

            Delete.Table("SharedBookListTags");
        }
    }
}