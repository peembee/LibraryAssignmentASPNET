﻿// <auto-generated />
using System;
using LibraryAssignment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryAssignment.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20230502110302_Capital")]
    partial class Capital
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryAssignment.Models.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookID"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Iban")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("RegisteredBookDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("BookID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryAssignment.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("LibraryAssignment.Models.CustomerBookList", b =>
                {
                    b.Property<int>("CustomerBookListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerBookListID"));

                    b.Property<DateTime>("EndBookedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FK_BookID")
                        .HasColumnType("int");

                    b.Property<int>("FK_CustomerID")
                        .HasColumnType("int");

                    b.Property<bool?>("Retrieved")
                        .HasColumnType("bit");

                    b.Property<bool?>("Returned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartBookedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CustomerBookListID");

                    b.HasIndex("FK_BookID");

                    b.HasIndex("FK_CustomerID");

                    b.ToTable("CustomerBookLists");
                });

            modelBuilder.Entity("LibraryAssignment.Models.CustomerBookList", b =>
                {
                    b.HasOne("LibraryAssignment.Models.Book", "books")
                        .WithMany("CustomerBookLists")
                        .HasForeignKey("FK_BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryAssignment.Models.Customer", "customers")
                        .WithMany("CustomerBookLists")
                        .HasForeignKey("FK_CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("books");

                    b.Navigation("customers");
                });

            modelBuilder.Entity("LibraryAssignment.Models.Book", b =>
                {
                    b.Navigation("CustomerBookLists");
                });

            modelBuilder.Entity("LibraryAssignment.Models.Customer", b =>
                {
                    b.Navigation("CustomerBookLists");
                });
#pragma warning restore 612, 618
        }
    }
}
