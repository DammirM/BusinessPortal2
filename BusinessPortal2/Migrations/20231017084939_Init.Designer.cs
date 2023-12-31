﻿// <auto-generated />
using System;
using BusinessPortal2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessPortal2.Migrations
{
    [DbContext(typeof(PersonaldataContext))]
    [Migration("20231017084939_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessPortal2.Models.LeaveRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApprovalState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateRequest")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LeaveTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PersonalId");

                    b.ToTable("leaveRequests");
                });

            modelBuilder.Entity("BusinessPortal2.Models.LeaveType", b =>
                {
                    b.Property<int>("PersonalId")
                        .HasColumnType("int");

                    b.Property<int>("Sick")
                        .HasColumnType("int");

                    b.Property<int>("Vabb")
                        .HasColumnType("int");

                    b.Property<int>("Vacation")
                        .HasColumnType("int");

                    b.HasKey("PersonalId");

                    b.ToTable("leaveTypes");
                });

            modelBuilder.Entity("BusinessPortal2.Models.Personal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("personals");
                });

            modelBuilder.Entity("BusinessPortal2.Models.LeaveRequest", b =>
                {
                    b.HasOne("BusinessPortal2.Models.Personal", "personal")
                        .WithMany("leaveRequests")
                        .HasForeignKey("PersonalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("personal");
                });

            modelBuilder.Entity("BusinessPortal2.Models.LeaveType", b =>
                {
                    b.HasOne("BusinessPortal2.Models.Personal", "Personal")
                        .WithOne("leavetype")
                        .HasForeignKey("BusinessPortal2.Models.LeaveType", "PersonalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("BusinessPortal2.Models.Personal", b =>
                {
                    b.Navigation("leaveRequests");

                    b.Navigation("leavetype");
                });
#pragma warning restore 612, 618
        }
    }
}
