﻿// <auto-generated />
using System;
using MVCSampleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVCSampleApp.Migrations
{
    [DbContext(typeof(ClientContext))]
    [Migration("20250201025425_EmployeeClient")]
    partial class EmployeeClient
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClientService", b =>
                {
                    b.Property<Guid>("ClientsID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServicesID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClientsID", "ServicesID");

                    b.HasIndex("ServicesID");

                    b.ToTable("ClientService");
                });

            modelBuilder.Entity("MVCSampleApp.Models.Person", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("People");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("MVCSampleApp.Models.Service", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("MVCSampleApp.Models.Client", b =>
                {
                    b.HasBaseType("MVCSampleApp.Models.Person");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("MVCSampleApp.Models.Employee", b =>
                {
                    b.HasBaseType("MVCSampleApp.Models.Person");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("ServiceID")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("ServiceID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ClientService", b =>
                {
                    b.HasOne("MVCSampleApp.Models.Client", null)
                        .WithMany()
                        .HasForeignKey("ClientsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCSampleApp.Models.Service", null)
                        .WithMany()
                        .HasForeignKey("ServicesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVCSampleApp.Models.Client", b =>
                {
                    b.HasOne("MVCSampleApp.Models.Person", null)
                        .WithOne()
                        .HasForeignKey("MVCSampleApp.Models.Client", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVCSampleApp.Models.Employee", b =>
                {
                    b.HasOne("MVCSampleApp.Models.Person", null)
                        .WithOne()
                        .HasForeignKey("MVCSampleApp.Models.Employee", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCSampleApp.Models.Service", "Service")
                        .WithMany("Employees")
                        .HasForeignKey("ServiceID");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("MVCSampleApp.Models.Service", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
