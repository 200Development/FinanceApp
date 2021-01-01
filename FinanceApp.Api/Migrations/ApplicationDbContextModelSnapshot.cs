﻿// <auto-generated />
using System;
using FinanceApp.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinanceApp.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FinanceApp.Api.Models.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("BalanceLimit")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("BalanceSurplus")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<bool>("ExcludeFromSurplus")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsAddNewAccount")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDisposableIncomeAccount")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsEmergencyFund")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsMandatory")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("PaycheckContribution")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("RequiredSavings")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("SuggestedPaycheckContribution")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("FinanceApp.Api.Models.Entities.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("AmountDue")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("PayDeduction")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("PaymentFrequency")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("FinanceApp.Api.Models.Entities.Bill", b =>
                {
                    b.HasOne("FinanceApp.Api.Models.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");
                });
#pragma warning restore 612, 618
        }
    }
}