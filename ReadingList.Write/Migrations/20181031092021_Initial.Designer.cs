﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ReadingList.Domain.Enumerations;
using ReadingList.Write;
using System;

namespace ReadingList.Write.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181031092021_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("ReadingList.Domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("GenreId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("Title", "Author")
                        .IsUnique();

                    b.ToTable("Books");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.BookList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("OwnerId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("BookLists");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.Genre", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ParentId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.HelpEntities.BookListModerator", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("BookListId");

                    b.HasKey("UserId", "BookListId");

                    b.HasIndex("BookListId");

                    b.ToTable("BookListModerators");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.HelpEntities.BookTag", b =>
                {
                    b.Property<int>("TagId");

                    b.Property<int>("BookId");

                    b.HasKey("TagId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookTags");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.HelpEntities.SharedBookListItemTag", b =>
                {
                    b.Property<int>("SharedBookListItemId");

                    b.Property<int>("TagId");

                    b.HasKey("SharedBookListItemId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("SharedBookListItemTags");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.HelpEntities.SharedBookListTag", b =>
                {
                    b.Property<int>("SharedBookListId");

                    b.Property<int>("TagId");

                    b.HasKey("SharedBookListId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("SharedBookListTags");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.Identity.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<int>("ProfileId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("ProfileId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.PrivateBookListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int>("BookListId");

                    b.Property<string>("GenreId");

                    b.Property<DateTime>("LastStatusUpdateDate");

                    b.Property<int>("ReadingTimeInSeconds");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BookListId");

                    b.HasIndex("GenreId");

                    b.ToTable("PrivateBookListItems");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.SharedBookListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int>("BookListId");

                    b.Property<string>("GenreId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BookListId");

                    b.HasIndex("GenreId");

                    b.ToTable("SharedBookListItems");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.Book", b =>
                {
                    b.HasOne("ReadingList.Domain.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.BookList", b =>
                {
                    b.HasOne("ReadingList.Domain.Entities.Identity.User", "Owner")
                        .WithMany("BookLists")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.Genre", b =>
                {
                    b.HasOne("ReadingList.Domain.Entities.Genre", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.HelpEntities.BookListModerator", b =>
                {
                    b.HasOne("ReadingList.Domain.Entities.BookList", "BookList")
                        .WithMany("BookListModerators")
                        .HasForeignKey("BookListId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ReadingList.Domain.Entities.Identity.User", "User")
                        .WithMany("BookListModerators")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.HelpEntities.BookTag", b =>
                {
                    b.HasOne("ReadingList.Domain.Entities.Book", "Book")
                        .WithMany("BookTags")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReadingList.Domain.Entities.Tag", "Tag")
                        .WithMany("BookTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.HelpEntities.SharedBookListItemTag", b =>
                {
                    b.HasOne("ReadingList.Domain.Entities.SharedBookListItem", "SharedBookListItem")
                        .WithMany("SharedBookListItemTags")
                        .HasForeignKey("SharedBookListItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReadingList.Domain.Entities.Tag", "Tag")
                        .WithMany("SharedBookListItemTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.HelpEntities.SharedBookListTag", b =>
                {
                    b.HasOne("ReadingList.Domain.Entities.BookList", "SharedBookList")
                        .WithMany("SharedBookListTags")
                        .HasForeignKey("SharedBookListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReadingList.Domain.Entities.Tag", "Tag")
                        .WithMany("SharedBookListTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.Identity.User", b =>
                {
                    b.HasOne("ReadingList.Domain.Entities.Identity.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReadingList.Domain.Entities.Identity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.PrivateBookListItem", b =>
                {
                    b.HasOne("ReadingList.Domain.Entities.BookList", "BookList")
                        .WithMany()
                        .HasForeignKey("BookListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReadingList.Domain.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("ReadingList.Domain.Entities.SharedBookListItem", b =>
                {
                    b.HasOne("ReadingList.Domain.Entities.BookList", "BookList")
                        .WithMany()
                        .HasForeignKey("BookListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReadingList.Domain.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");
                });
#pragma warning restore 612, 618
        }
    }
}
