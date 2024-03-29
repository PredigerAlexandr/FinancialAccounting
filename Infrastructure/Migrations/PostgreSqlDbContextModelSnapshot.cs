﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(PostgreSqlDbContext))]
    partial class PostgreSqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.BankDeposit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("CurrentSum")
                        .HasColumnType("numeric");

                    b.Property<decimal>("FullSum")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId1");

                    b.ToTable("BankDeposits");
                });

            modelBuilder.Entity("Domain.Entities.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("CurrentSum")
                        .HasColumnType("numeric");

                    b.Property<decimal>("FullSum")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Salary")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.BankDeposit", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("BankDeposits")
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Loan", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("BankDeposits");

                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
