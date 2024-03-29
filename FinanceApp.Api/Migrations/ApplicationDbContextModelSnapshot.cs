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
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("BalanceLimit")
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
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

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

            modelBuilder.Entity("FinanceApp.Api.Models.Entities.Expense", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("AmountDue")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatePaid")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsBill")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("Paid")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("PayDeduction")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("PaymentFrequency")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("FinanceApp.Api.Models.Entities.Income", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("FirstMonthlyPayDay")
                        .HasColumnType("int");

                    b.Property<DateTime>("NextPayday")
                        .HasColumnType("datetime");

                    b.Property<string>("Payee")
                        .HasColumnType("text");

                    b.Property<int>("PaymentFrequency")
                        .HasColumnType("int");

                    b.Property<int?>("SecondMonthlyPayDay")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("FinanceApp.Api.Models.Entities.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Payee")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("FinanceApp.Api.Models.Entities.Bill", b =>
                {
                    b.HasOne("FinanceApp.Api.Models.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinanceApp.Api.Models.Entities.Expense", b =>
                {
                    b.HasOne("FinanceApp.Api.Models.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
