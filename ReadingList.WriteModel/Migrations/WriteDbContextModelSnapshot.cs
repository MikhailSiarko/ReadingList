﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using System;

namespace ReadingList.WriteModel.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    partial class WriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReadingList.WriteModel.Models.BookListWm", b =>
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

            modelBuilder.Entity("ReadingList.WriteModel.Models.BookWm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("GenreId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("Title", "Author")
                        .IsUnique()
                        .HasFilter("[Title] IS NOT NULL AND [Author] IS NOT NULL");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.GenreWm", b =>
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

            modelBuilder.Entity("ReadingList.WriteModel.Models.HelpEntities.BookListModeratorWm", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("BookListId");

                    b.HasKey("UserId", "BookListId");

                    b.HasIndex("BookListId");

                    b.ToTable("BookListModerators");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.HelpEntities.BookTagWm", b =>
                {
                    b.Property<int>("TagId");

                    b.Property<int>("BookId");

                    b.HasKey("TagId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookTags");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.HelpEntities.SharedBookListItemTagWm", b =>
                {
                    b.Property<int>("SharedBookListItemId");

                    b.Property<int>("TagId");

                    b.HasKey("SharedBookListItemId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("SharedBookListItemTags");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.HelpEntities.SharedBookListTagWm", b =>
                {
                    b.Property<int>("SharedBookListId");

                    b.Property<int>("TagId");

                    b.HasKey("SharedBookListId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("SharedBookListTags");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.PrivateBookListItemWm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int>("BookListId");

                    b.Property<DateTime>("LastStatusUpdateDate");

                    b.Property<int>("ReadingTimeInSeconds");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BookListId");

                    b.ToTable("PrivateBookListItems");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.ProfileWm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.RoleWm", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.SharedBookListItemWm", b =>
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

            modelBuilder.Entity("ReadingList.WriteModel.Models.TagWm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.UserWm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<int>("ProfileId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasFilter("[Login] IS NOT NULL");

                    b.HasIndex("ProfileId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.BookListWm", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.UserWm", "Owner")
                        .WithMany("BookLists")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.BookWm", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.GenreWm", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.GenreWm", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.GenreWm", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.HelpEntities.BookListModeratorWm", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.BookListWm", "BookList")
                        .WithMany("BookListModerators")
                        .HasForeignKey("BookListId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ReadingList.WriteModel.Models.UserWm", "User")
                        .WithMany("BookListModerators")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.HelpEntities.BookTagWm", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.BookWm", "Book")
                        .WithMany("BookTags")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReadingList.WriteModel.Models.TagWm", "Tag")
                        .WithMany("BookTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.HelpEntities.SharedBookListItemTagWm", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.SharedBookListItemWm", "SharedBookListItem")
                        .WithMany("SharedBookListItemTags")
                        .HasForeignKey("SharedBookListItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReadingList.WriteModel.Models.TagWm", "Tag")
                        .WithMany("SharedBookListItemTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.HelpEntities.SharedBookListTagWm", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.BookListWm", "SharedBookList")
                        .WithMany("SharedBookListTags")
                        .HasForeignKey("SharedBookListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReadingList.WriteModel.Models.TagWm", "Tag")
                        .WithMany("SharedBookListTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.PrivateBookListItemWm", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.BookListWm", "BookList")
                        .WithMany()
                        .HasForeignKey("BookListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.SharedBookListItemWm", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.BookListWm", "BookList")
                        .WithMany()
                        .HasForeignKey("BookListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReadingList.WriteModel.Models.GenreWm", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.UserWm", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.ProfileWm", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
