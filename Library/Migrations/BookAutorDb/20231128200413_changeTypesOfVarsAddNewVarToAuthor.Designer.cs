﻿// <auto-generated />
using System;
using Library.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Migrations.BookAutorDb
{
    [DbContext(typeof(BookAutorDbContext))]
    [Migration("20231128200413_changeTypesOfVarsAddNewVarToAuthor")]
    partial class changeTypesOfVarsAddNewVarToAuthor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Library.Domains.BookAuthor.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DateOfBirthday")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkToYaBook")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkToYaBook")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("YearOfCreation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.BookAuthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.BookType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Img")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.IsBookHearted", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<bool>("IsHearted")
                        .HasColumnType("bit");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("IsBooksHearted");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<int>("PublishedHouseId")
                        .HasColumnType("int");

                    b.Property<int?>("PublishingHouseId")
                        .HasColumnType("int");

                    b.Property<int>("YearOfPublication")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PublishingHouseId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.MoreInfoAboutBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("Illustrations")
                        .HasColumnType("bit");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("TypeOfCover")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId")
                        .IsUnique();

                    b.ToTable("MoreInfoAboutBooks");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.PublishingHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("PublishingHouseName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("PublishingHouses");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.BookAuthor", b =>
                {
                    b.HasOne("Library.Domains.BookAuthor.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Domains.BookAuthor.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.BookType", b =>
                {
                    b.HasOne("Library.Domains.BookAuthor.Book", null)
                        .WithMany("Types")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.Image", b =>
                {
                    b.HasOne("Library.Domains.BookAuthor.Book", null)
                        .WithMany("Images")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.IsBookHearted", b =>
                {
                    b.HasOne("Library.Domains.BookAuthor.Book", "Book")
                        .WithMany("IsBooksHearted")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.Language", b =>
                {
                    b.HasOne("Library.Domains.BookAuthor.PublishingHouse", null)
                        .WithMany("Languages")
                        .HasForeignKey("PublishingHouseId");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.MoreInfoAboutBook", b =>
                {
                    b.HasOne("Library.Domains.BookAuthor.Language", null)
                        .WithOne("MoreInfo")
                        .HasForeignKey("Library.Domains.BookAuthor.MoreInfoAboutBook", "LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.PublishingHouse", b =>
                {
                    b.HasOne("Library.Domains.BookAuthor.Book", null)
                        .WithMany("PublishingHouses")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.Book", b =>
                {
                    b.Navigation("BookAuthors");

                    b.Navigation("Images");

                    b.Navigation("IsBooksHearted");

                    b.Navigation("PublishingHouses");

                    b.Navigation("Types");
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.Language", b =>
                {
                    b.Navigation("MoreInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("Library.Domains.BookAuthor.PublishingHouse", b =>
                {
                    b.Navigation("Languages");
                });
#pragma warning restore 612, 618
        }
    }
}
