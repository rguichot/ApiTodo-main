﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiTodo.Migrations
{
    [DbContext(typeof(TodoContext))]
    [Migration("20231016141539_AddListToDb")]
    partial class AddListToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("MvcTodo.Models.List", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Lists");
                });

            modelBuilder.Entity("MvcTodo.Models.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Completed")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ListId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Task")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ListId");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("MvcTodo.Models.Todo", b =>
                {
                    b.HasOne("MvcTodo.Models.List", "List")
                        .WithMany()
                        .HasForeignKey("ListId");

                    b.Navigation("List");
                });
#pragma warning restore 612, 618
        }
    }
}
