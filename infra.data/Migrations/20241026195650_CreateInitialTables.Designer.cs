﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalFinanceManagement.Infra.Data.Contexts;

#nullable disable

namespace PersonalFinanceManagement.Infra.Data.Migrations
{
    [DbContext(typeof(DefaultDBContext))]
    [Migration("20241026195650_CreateInitialTables")]
    partial class CreateInitialTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Balances.Entities.Balance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(9);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnOrder(4);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(11);

                    b.Property<bool>("Financed")
                        .HasColumnType("bit")
                        .HasColumnOrder(6);

                    b.Property<short>("InstallmentsNumber")
                        .HasColumnType("smallint")
                        .HasColumnOrder(7);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnOrder(2);

                    b.Property<bool>("Residue")
                        .HasColumnType("bit")
                        .HasColumnOrder(8);

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(10);

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Balances", (string)null);
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Balances.Entities.Installment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnOrder(6);

                    b.Property<int>("BalanceId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(7);

                    b.Property<int?>("ManagementId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<short>("Number")
                        .HasColumnType("smallint")
                        .HasColumnOrder(4);

                    b.Property<int>("Reference")
                        .HasColumnType("int")
                        .HasColumnOrder(3);

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("BalanceId");

                    b.ToTable("Installments", (string)null);
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Balances.Entities.RefinancedBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnOrder(10);

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnOrder(7);

                    b.Property<int>("BalanceId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(11);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnOrder(6);

                    b.Property<bool>("Financed")
                        .HasColumnType("bit")
                        .HasColumnOrder(8);

                    b.Property<short>("InstallmentsNumber")
                        .HasColumnType("smallint")
                        .HasColumnOrder(9);

                    b.Property<decimal>("OriginalAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("OriginalDate")
                        .HasColumnType("date")
                        .HasColumnOrder(2);

                    b.Property<bool>("OriginalFinanced")
                        .HasColumnType("bit")
                        .HasColumnOrder(4);

                    b.Property<short>("OriginalInstallmentsNumber")
                        .HasColumnType("smallint")
                        .HasColumnOrder(5);

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnOrder(12);

                    b.HasKey("Id");

                    b.HasIndex("BalanceId");

                    b.HasIndex("UserId");

                    b.ToTable("RefinancedBalances", (string)null);
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Transactions.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(8);

                    b.Property<int?>("ManagementId")
                        .HasColumnType("int");

                    b.Property<int>("Reference")
                        .HasColumnType("int")
                        .HasColumnOrder(4);

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(7);

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Transactions.Entities.TransactionItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("AmountPaid")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnOrder(5);

                    b.Property<int>("InstallmentId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<bool>("PartiallyPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnOrder(4);

                    b.Property<int>("TransactionId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("Type")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnOrder(3);

                    b.HasKey("Id");

                    b.HasIndex("InstallmentId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionItems", (string)null);
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Transactions.Entities.TransactionResidue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("TransactionItemId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int>("TransactionItemOriginId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("TransactionItemId")
                        .IsUnique();

                    b.HasIndex("TransactionItemOriginId");

                    b.ToTable("TransactionResidue", (string)null);
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Users.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnOrder(1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnOrder(4);

                    b.Property<DateTime?>("RefeshTokenExperitionTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(7);

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(6);

                    b.Property<short>("Role")
                        .HasColumnType("smallint")
                        .HasColumnOrder(5);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Balances.Entities.Balance", b =>
                {
                    b.HasOne("PersonalFinanceManagement.Domain.Users.Entities.User", "User")
                        .WithMany("Balances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Balances.Entities.Installment", b =>
                {
                    b.HasOne("PersonalFinanceManagement.Domain.Balances.Entities.Balance", "Balance")
                        .WithMany("Installments")
                        .HasForeignKey("BalanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Balance");
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Balances.Entities.RefinancedBalance", b =>
                {
                    b.HasOne("PersonalFinanceManagement.Domain.Balances.Entities.Balance", "Balance")
                        .WithMany("RefinancedBalances")
                        .HasForeignKey("BalanceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PersonalFinanceManagement.Domain.Users.Entities.User", "User")
                        .WithMany("RefinancedBalances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Balance");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Transactions.Entities.Transaction", b =>
                {
                    b.HasOne("PersonalFinanceManagement.Domain.Users.Entities.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Transactions.Entities.TransactionItem", b =>
                {
                    b.HasOne("PersonalFinanceManagement.Domain.Balances.Entities.Installment", "Installment")
                        .WithMany("TransactionItems")
                        .HasForeignKey("InstallmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PersonalFinanceManagement.Domain.Transactions.Entities.Transaction", "Transaction")
                        .WithMany("TransactionItems")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Installment");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Transactions.Entities.TransactionResidue", b =>
                {
                    b.HasOne("PersonalFinanceManagement.Domain.Transactions.Entities.TransactionItem", "TransactionItem")
                        .WithOne()
                        .HasForeignKey("PersonalFinanceManagement.Domain.Transactions.Entities.TransactionResidue", "TransactionItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PersonalFinanceManagement.Domain.Transactions.Entities.TransactionItem", "TransactionItemOrigin")
                        .WithMany("TransactionResidues")
                        .HasForeignKey("TransactionItemOriginId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TransactionItem");

                    b.Navigation("TransactionItemOrigin");
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Balances.Entities.Balance", b =>
                {
                    b.Navigation("Installments");

                    b.Navigation("RefinancedBalances");
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Balances.Entities.Installment", b =>
                {
                    b.Navigation("TransactionItems");
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Transactions.Entities.Transaction", b =>
                {
                    b.Navigation("TransactionItems");
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Transactions.Entities.TransactionItem", b =>
                {
                    b.Navigation("TransactionResidues");
                });

            modelBuilder.Entity("PersonalFinanceManagement.Domain.Users.Entities.User", b =>
                {
                    b.Navigation("Balances");

                    b.Navigation("RefinancedBalances");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
