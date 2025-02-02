﻿// <auto-generated />
using System;
using HappyX.Infrastructure.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HappyX.Api.Migrations
{
    [DbContext(typeof(HappyXContext))]
    [Migration("20220920175631_NewMigration")]
    partial class NewMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HappyX.Domain.Internal.Mood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("moods", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "sad"
                        },
                        new
                        {
                            Id = 2,
                            Name = "unhappy"
                        },
                        new
                        {
                            Id = 3,
                            Name = "indifferent"
                        },
                        new
                        {
                            Id = 4,
                            Name = "happy"
                        },
                        new
                        {
                            Id = 5,
                            Name = "joyful"
                        });
                });

            modelBuilder.Entity("HappyX.Domain.Internal.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<int>("MoodId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MoodId");

                    b.HasIndex(new[] { "UserId", "CreationDate" }, "unique_user_date")
                        .IsUnique();

                    b.ToTable("records", (string)null);
                });

            modelBuilder.Entity("HappyX.Domain.Internal.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("SlackId")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("slack_id");

                    b.Property<bool>("Subscribed")
                        .HasColumnType("boolean")
                        .HasColumnName("subscribed");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "SlackId" }, "unique_slack_id")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("HappyX.Domain.Internal.Record", b =>
                {
                    b.HasOne("HappyX.Domain.Internal.Mood", "Mood")
                        .WithMany("Records")
                        .HasForeignKey("MoodId")
                        .IsRequired()
                        .HasConstraintName("moods_fk");

                    b.HasOne("HappyX.Domain.Internal.User", "User")
                        .WithMany("Records")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("users_fk");

                    b.Navigation("Mood");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HappyX.Domain.Internal.Mood", b =>
                {
                    b.Navigation("Records");
                });

            modelBuilder.Entity("HappyX.Domain.Internal.User", b =>
                {
                    b.Navigation("Records");
                });
#pragma warning restore 612, 618
        }
    }
}
