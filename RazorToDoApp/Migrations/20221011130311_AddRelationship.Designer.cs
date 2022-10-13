﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RazorToDoApp.Data;

#nullable disable

namespace RazorToDoApp.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20221011130311_AddRelationship")]
    partial class AddRelationship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RazorToDoApp.Models.DbToDoLists", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DbUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("DbUserId");

                    b.ToTable("ToDoLists");
                });

            modelBuilder.Entity("RazorToDoApp.Models.DbUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RazorToDoApp.Models.DbToDoLists", b =>
                {
                    b.HasOne("RazorToDoApp.Models.DbUser", null)
                        .WithMany("DbToDoLists")
                        .HasForeignKey("DbUserId");
                });

            modelBuilder.Entity("RazorToDoApp.Models.DbUser", b =>
                {
                    b.Navigation("DbToDoLists");
                });
#pragma warning restore 612, 618
        }
    }
}
