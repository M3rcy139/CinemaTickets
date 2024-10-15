﻿// <auto-generated />
using System;
using CinemaTickets.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CinemaTickets.Persistence.Migrations
{
    [DbContext(typeof(CinemaDbContext))]
    [Migration("20241015123727_initial1")]
    partial class initial1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.HallEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Halls", (string)null);
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.IssueReportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime>("MessageTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("IssueReports", (string)null);
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.PaymentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("ChangeGiven")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("PaymentTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("SeatId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.SeanceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FilmName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("HallId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.ToTable("Seances", (string)null);
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.SeatEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("HallId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("RowNumber")
                        .HasColumnType("integer");

                    b.Property<int>("SeanceId")
                        .HasColumnType("integer");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("SeanceId")
                        .IsUnique();

                    b.ToTable("Seats", (string)null);
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.TicketEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FilmName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FilmStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HallName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("IssueTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("RowNumber")
                        .HasColumnType("integer");

                    b.Property<int>("SeatId")
                        .HasColumnType("integer");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserEntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserSurname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.HasIndex("SeatId");

                    b.HasIndex("UserEntityId");

                    b.ToTable("Tickets", (string)null);
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.PaymentEntity", b =>
                {
                    b.HasOne("CinemaTickets.Persistence.Entities.UserEntity", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.SeanceEntity", b =>
                {
                    b.HasOne("CinemaTickets.Persistence.Entities.HallEntity", "Hall")
                        .WithMany("Seances")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.SeatEntity", b =>
                {
                    b.HasOne("CinemaTickets.Persistence.Entities.HallEntity", "Hall")
                        .WithMany("Seats")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaTickets.Persistence.Entities.PaymentEntity", "Payment")
                        .WithMany("Seats")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CinemaTickets.Persistence.Entities.SeanceEntity", "Seance")
                        .WithOne()
                        .HasForeignKey("CinemaTickets.Persistence.Entities.SeatEntity", "SeanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");

                    b.Navigation("Payment");

                    b.Navigation("Seance");
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.TicketEntity", b =>
                {
                    b.HasOne("CinemaTickets.Persistence.Entities.PaymentEntity", "Payment")
                        .WithMany("Tickets")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaTickets.Persistence.Entities.SeatEntity", "Seat")
                        .WithMany()
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaTickets.Persistence.Entities.UserEntity", null)
                        .WithMany("Tickets")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Payment");

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.HallEntity", b =>
                {
                    b.Navigation("Seances");

                    b.Navigation("Seats");
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.PaymentEntity", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("CinemaTickets.Persistence.Entities.UserEntity", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
