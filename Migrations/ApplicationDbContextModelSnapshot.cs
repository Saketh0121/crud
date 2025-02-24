﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sunny_Kasuvojula_A3__11_1_S4.Models;

#nullable disable

namespace Sunny_Kasuvojula_A3__11_1_S4.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sunny_Kasuvojula_A3__11_1_S4.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfHire")
                        .HasColumnType("datetime2");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            DOB = new DateTime(1956, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfHire = new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Firstname = "Ada",
                            Lastname = "Lovelace"
                        });
                });

            modelBuilder.Entity("Sunny_Kasuvojula_A3__11_1_S4.Models.Sales", b =>
                {
                    b.Property<int>("SalesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalesId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Quarter")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("SalesId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Sales");

                    b.HasData(
                        new
                        {
                            SalesId = 1,
                            Amount = 5000.0,
                            EmployeeId = 1,
                            Quarter = 1,
                            Year = 2024
                        });
                });

            modelBuilder.Entity("Sunny_Kasuvojula_A3__11_1_S4.Models.Employee", b =>
                {
                    b.HasOne("Sunny_Kasuvojula_A3__11_1_S4.Models.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Sunny_Kasuvojula_A3__11_1_S4.Models.Sales", b =>
                {
                    b.HasOne("Sunny_Kasuvojula_A3__11_1_S4.Models.Employee", "Employee")
                        .WithMany("Sales")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Sunny_Kasuvojula_A3__11_1_S4.Models.Employee", b =>
                {
                    b.Navigation("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}
