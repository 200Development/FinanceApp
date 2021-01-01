﻿// <auto-generated />
using FinanceApp.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinanceApp.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201220191301_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FinanceApp.Api.Entities.Account", b =>
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
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}