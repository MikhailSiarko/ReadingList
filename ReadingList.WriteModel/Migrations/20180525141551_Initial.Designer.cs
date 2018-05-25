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
    [DbContext(typeof(MigrationDbContext))]
    [Migration("20180525141551_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReadingList.WriteModel.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("Author")
                        .IsUnique()
                        .HasFilter("[Author] IS NOT NULL");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasFilter("[Title] IS NOT NULL");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.BookList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("JsonFields");

                    b.Property<string>("Name");

                    b.Property<int>("OwnerId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("BookLists");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.PrivateBookListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int>("BookListId");

                    b.Property<long>("ReadingTimeInTicks");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BookListId");

                    b.ToTable("PrivateBookListItems");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.Profile", b =>
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

            modelBuilder.Entity("ReadingList.WriteModel.Models.Role", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.SharedBookListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int>("BookListId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BookListId");

                    b.ToTable("SharedBookListItems");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.User", b =>
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

            modelBuilder.Entity("ReadingList.WriteModel.Models.BookList", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.User", "Owner")
                        .WithMany("BookLists")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.PrivateBookListItem", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.BookList", "BookList")
                        .WithMany()
                        .HasForeignKey("BookListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.SharedBookListItem", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.BookList", "BookList")
                        .WithMany()
                        .HasForeignKey("BookListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReadingList.WriteModel.Models.User", b =>
                {
                    b.HasOne("ReadingList.WriteModel.Models.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
