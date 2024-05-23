﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBank.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyBank.Persistence.Migrations
{
    [DbContext(typeof(MyBankDbContext))]
    partial class MyBankDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MyBank.Domain.Models.CreditPackageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CreditGrantedAmount")
                        .HasColumnType("money");

                    b.Property<decimal>("CreditStartBalance")
                        .HasColumnType("money");

                    b.Property<int>("CreditTermInDays")
                        .HasColumnType("integer");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<bool>("HasPrepaymentOption")
                        .HasColumnType("boolean");

                    b.Property<string>("InterestCalculationType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("CreditPackages");
                });

            modelBuilder.Entity("MyBank.Domain.Models.DepositPackageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<decimal>("DepositStartBalance")
                        .HasColumnType("money");

                    b.Property<int>("DepositTermInDays")
                        .HasColumnType("integer");

                    b.Property<bool>("HasCapitalisation")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasInterestWithdrawalOption")
                        .HasColumnType("boolean");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRevocable")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("DepositPackages");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.AdminEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.BankSettingsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CreditInterestRate")
                        .HasColumnType("numeric");

                    b.Property<decimal>("DepositInterestRate")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("BankSettings");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CardEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CardPackageId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CvvCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOperationsAllowed")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PersonalAccountId")
                        .HasColumnType("integer");

                    b.Property<string>("Pincode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CardPackageId");

                    b.HasIndex("PersonalAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CardPackageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AverageAccountBalance")
                        .HasColumnType("money");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OperationsNumber")
                        .HasColumnType("integer");

                    b.Property<decimal>("OperationsSum")
                        .HasColumnType("money");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("CardPackages");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CreditAccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ClosingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("CreditGrantedAmount")
                        .HasColumnType("money");

                    b.Property<decimal>("CreditStartBalance")
                        .HasColumnType("money");

                    b.Property<int>("CreditTermInDays")
                        .HasColumnType("integer");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("money");

                    b.Property<bool>("HasPrepaymentOption")
                        .HasColumnType("boolean");

                    b.Property<string>("InterestCalculationType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("MadePaymentsNumber")
                        .HasColumnType("integer");

                    b.Property<int?>("ModeratorApprovedId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalPaymentsNumber")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("ModeratorApprovedId");

                    b.HasIndex("UserId");

                    b.ToTable("CreditAccounts");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CreditPaymentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreditAccountId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("money");

                    b.Property<int>("PaymentNumber")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreditAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("CreditPayments");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CreditRequestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CreditPackageId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<int?>("ModeratorId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreditPackageId");

                    b.HasIndex("ModeratorId");

                    b.HasIndex("UserId");

                    b.ToTable("CreditRequests");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CurrencyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastRateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("OfficialRate")
                        .HasColumnType("money");

                    b.Property<int>("Scale")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.DepositAccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ClosingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("money");

                    b.Property<decimal>("DepositStartBalance")
                        .HasColumnType("money");

                    b.Property<int>("DepositTermInDays")
                        .HasColumnType("integer");

                    b.Property<bool>("HasCapitalisation")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasInterestWithdrawalOption")
                        .HasColumnType("boolean");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRevocable")
                        .HasColumnType("boolean");

                    b.Property<int>("MadeAccrualsNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalAccrualsNumber")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("DepositAccounts");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.DepositAccrualEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AccrualAmount")
                        .HasColumnType("money");

                    b.Property<int>("AccrualNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DepositAccountId")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("DepositAccountId");

                    b.ToTable("DepositAccruals");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.MessageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDatetime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<int>("RecepientId")
                        .HasColumnType("integer");

                    b.Property<string>("RecepientNickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RecepientRole")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SenderAdminId")
                        .HasColumnType("integer");

                    b.Property<int?>("SenderModeratorId")
                        .HasColumnType("integer");

                    b.Property<int?>("SenderUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SenderAdminId");

                    b.HasIndex("SenderModeratorId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.ModeratorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Moderators");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.PersonalAccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ClosingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("money");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsForTransfersByNickname")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("PersonalAccounts");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.TransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountRecipientNumber")
                        .HasColumnType("text");

                    b.Property<string>("AccountSenderNumber")
                        .HasColumnType("text");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("numeric");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<string>("UserRecipientNickname")
                        .HasColumnType("text");

                    b.Property<string>("UserSenderNickname")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AvatarImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthdayDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Citizenship")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CreditRequestsRejected")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PassportSeries")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyBank.Domain.Models.CreditPackageEntity", b =>
                {
                    b.HasOne("MyBank.Persistence.Entities.CurrencyEntity", "Currency")
                        .WithMany("CreditPackages")
                        .HasForeignKey("CurrencyId");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("MyBank.Domain.Models.DepositPackageEntity", b =>
                {
                    b.HasOne("MyBank.Persistence.Entities.CurrencyEntity", "Currency")
                        .WithMany("DepositPackages")
                        .HasForeignKey("CurrencyId");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CardEntity", b =>
                {
                    b.HasOne("MyBank.Persistence.Entities.CardPackageEntity", "CardPackage")
                        .WithMany("Cards")
                        .HasForeignKey("CardPackageId");

                    b.HasOne("MyBank.Persistence.Entities.PersonalAccountEntity", "PersonalAccount")
                        .WithMany("Cards")
                        .HasForeignKey("PersonalAccountId");

                    b.HasOne("MyBank.Persistence.Entities.UserEntity", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId");

                    b.Navigation("CardPackage");

                    b.Navigation("PersonalAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CreditAccountEntity", b =>
                {
                    b.HasOne("MyBank.Persistence.Entities.CurrencyEntity", "Currency")
                        .WithMany("CreditAccounts")
                        .HasForeignKey("CurrencyId");

                    b.HasOne("MyBank.Persistence.Entities.ModeratorEntity", "ModeratorApproved")
                        .WithMany("CreditsApproved")
                        .HasForeignKey("ModeratorApprovedId");

                    b.HasOne("MyBank.Persistence.Entities.UserEntity", "User")
                        .WithMany("CreditAccounts")
                        .HasForeignKey("UserId");

                    b.Navigation("Currency");

                    b.Navigation("ModeratorApproved");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CreditPaymentEntity", b =>
                {
                    b.HasOne("MyBank.Persistence.Entities.CreditAccountEntity", "CreditAccount")
                        .WithMany("Payments")
                        .HasForeignKey("CreditAccountId");

                    b.HasOne("MyBank.Persistence.Entities.UserEntity", "User")
                        .WithMany("CreditPayments")
                        .HasForeignKey("UserId");

                    b.Navigation("CreditAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CreditRequestEntity", b =>
                {
                    b.HasOne("MyBank.Domain.Models.CreditPackageEntity", "CreditPackage")
                        .WithMany("CreditRequests")
                        .HasForeignKey("CreditPackageId");

                    b.HasOne("MyBank.Persistence.Entities.ModeratorEntity", "Moderator")
                        .WithMany("CreditRequestsReplied")
                        .HasForeignKey("ModeratorId");

                    b.HasOne("MyBank.Persistence.Entities.UserEntity", "User")
                        .WithMany("CreditRequests")
                        .HasForeignKey("UserId");

                    b.Navigation("CreditPackage");

                    b.Navigation("Moderator");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.DepositAccountEntity", b =>
                {
                    b.HasOne("MyBank.Persistence.Entities.CurrencyEntity", "Currency")
                        .WithMany("DepositAccounts")
                        .HasForeignKey("CurrencyId");

                    b.HasOne("MyBank.Persistence.Entities.UserEntity", "User")
                        .WithMany("DepositAccounts")
                        .HasForeignKey("UserId");

                    b.Navigation("Currency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.DepositAccrualEntity", b =>
                {
                    b.HasOne("MyBank.Persistence.Entities.DepositAccountEntity", "DepositAccount")
                        .WithMany("Accruals")
                        .HasForeignKey("DepositAccountId");

                    b.Navigation("DepositAccount");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.MessageEntity", b =>
                {
                    b.HasOne("MyBank.Persistence.Entities.AdminEntity", "SenderAdmin")
                        .WithMany("Messages")
                        .HasForeignKey("SenderAdminId");

                    b.HasOne("MyBank.Persistence.Entities.ModeratorEntity", "SenderModerator")
                        .WithMany("Messages")
                        .HasForeignKey("SenderModeratorId");

                    b.HasOne("MyBank.Persistence.Entities.UserEntity", "SenderUser")
                        .WithMany("Messages")
                        .HasForeignKey("SenderUserId");

                    b.Navigation("SenderAdmin");

                    b.Navigation("SenderModerator");

                    b.Navigation("SenderUser");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.PersonalAccountEntity", b =>
                {
                    b.HasOne("MyBank.Persistence.Entities.CurrencyEntity", "Currency")
                        .WithMany("PersonalAccounts")
                        .HasForeignKey("CurrencyId");

                    b.HasOne("MyBank.Persistence.Entities.UserEntity", "User")
                        .WithMany("PersonalAccounts")
                        .HasForeignKey("UserId");

                    b.Navigation("Currency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyBank.Domain.Models.CreditPackageEntity", b =>
                {
                    b.Navigation("CreditRequests");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.AdminEntity", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CardPackageEntity", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CreditAccountEntity", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.CurrencyEntity", b =>
                {
                    b.Navigation("CreditAccounts");

                    b.Navigation("CreditPackages");

                    b.Navigation("DepositAccounts");

                    b.Navigation("DepositPackages");

                    b.Navigation("PersonalAccounts");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.DepositAccountEntity", b =>
                {
                    b.Navigation("Accruals");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.ModeratorEntity", b =>
                {
                    b.Navigation("CreditRequestsReplied");

                    b.Navigation("CreditsApproved");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.PersonalAccountEntity", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("MyBank.Persistence.Entities.UserEntity", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("CreditAccounts");

                    b.Navigation("CreditPayments");

                    b.Navigation("CreditRequests");

                    b.Navigation("DepositAccounts");

                    b.Navigation("Messages");

                    b.Navigation("PersonalAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
