﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RenadWebApp.DTOModels;

#nullable disable

namespace RenadWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240605022304_login")]
    partial class login
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RenadWebApp.Models.DataModel.ClientContract", b =>
                {
                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.HasKey("ContractId", "ClientId");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientContract");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.ClientModels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AmountLate")
                        .HasColumnType("int");

                    b.Property<string>("Approved")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfProject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RestFromAmount")
                        .HasColumnType("int");

                    b.Property<int?>("TotalAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.ContractModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AmountLate")
                        .HasColumnType("int");

                    b.Property<string>("Approved")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("ProjectBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RatioForEng")
                        .HasColumnType("int");

                    b.Property<int?>("RestOfContractShow")
                        .HasColumnType("int");

                    b.Property<int?>("RestOfEngShow")
                        .HasColumnType("int");

                    b.Property<int?>("RestOfTAxShow")
                        .HasColumnType("int");

                    b.Property<int?>("TotalAmount")
                        .HasColumnType("int");

                    b.Property<int?>("TotalOfContract")
                        .HasColumnType("int");

                    b.Property<int?>("TotalRatioForEng")
                        .HasColumnType("int");

                    b.Property<int?>("TotalTax")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.ContractPayment", b =>
                {
                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.HasKey("ContractId", "PaymentId");

                    b.HasIndex("PaymentId");

                    b.ToTable("ContractPayment");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.EngContract", b =>
                {
                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<int>("EngId")
                        .HasColumnType("int");

                    b.HasKey("ContractId", "EngId");

                    b.HasIndex("EngId");

                    b.ToTable("EngContract");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.EngModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompOrFree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Connection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EngName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastCommunication")
                        .HasColumnType("datetime2");

                    b.Property<string>("Meeting")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Offices")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeOfMeeting")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Eng");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.FinaicalModels.ContractFinaical", b =>
                {
                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<int>("FinaicalId")
                        .HasColumnType("int");

                    b.HasKey("ContractId", "FinaicalId");

                    b.HasIndex("FinaicalId");

                    b.ToTable("ContractFinaical");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.FinaicalModels.FinaicalRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FinaicalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FinaicalRequests");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.LoginModel", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"));

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.PaymentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateForPayment")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Difference")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DueEngRatio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DueTax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LateTime1")
                        .HasColumnType("int");

                    b.Property<int?>("PayMentValue")
                        .HasColumnType("int");

                    b.Property<string>("PaymentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PaymentTax")
                        .HasColumnType("int");

                    b.Property<int?>("Payments")
                        .HasColumnType("int");

                    b.Property<int?>("RatioForEng")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.ClientContract", b =>
                {
                    b.HasOne("RenadWebApp.Models.DataModel.ClientModels", "Client")
                        .WithMany("ClientContract")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RenadWebApp.Models.DataModel.ContractModel", "Contract")
                        .WithMany("ClientContract")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.ContractPayment", b =>
                {
                    b.HasOne("RenadWebApp.Models.DataModel.ContractModel", "Contract")
                        .WithMany("ContractPayment")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RenadWebApp.Models.DataModel.PaymentModel", "Payment")
                        .WithMany("ContractPayment")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.EngContract", b =>
                {
                    b.HasOne("RenadWebApp.Models.DataModel.ContractModel", "Contract")
                        .WithMany("EngContract")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RenadWebApp.Models.DataModel.EngModel", "Eng")
                        .WithMany("EngContract")
                        .HasForeignKey("EngId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Eng");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.FinaicalModels.ContractFinaical", b =>
                {
                    b.HasOne("RenadWebApp.Models.DataModel.ContractModel", "Contract")
                        .WithMany("ContractFinaical")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RenadWebApp.Models.DataModel.FinaicalModels.FinaicalRequest", "Finaical")
                        .WithMany("ContractFinaical")
                        .HasForeignKey("FinaicalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Finaical");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.ClientModels", b =>
                {
                    b.Navigation("ClientContract");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.ContractModel", b =>
                {
                    b.Navigation("ClientContract");

                    b.Navigation("ContractFinaical");

                    b.Navigation("ContractPayment");

                    b.Navigation("EngContract");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.EngModel", b =>
                {
                    b.Navigation("EngContract");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.FinaicalModels.FinaicalRequest", b =>
                {
                    b.Navigation("ContractFinaical");
                });

            modelBuilder.Entity("RenadWebApp.Models.DataModel.PaymentModel", b =>
                {
                    b.Navigation("ContractPayment");
                });
#pragma warning restore 612, 618
        }
    }
}
