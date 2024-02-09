﻿// <auto-generated />
using System;
using HyperChatApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HyperChatApp.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HyperChatApp.Core.Aggregates.MessageAggregate.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("nvarchar(160)");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "RoomId" }, "IDX_MESSAGE_ROOMID");

                    b.HasIndex(new[] { "Time" }, "IDX_MESSAGE_TIME_DESC")
                        .IsDescending();

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("HyperChatApp.Core.Aggregates.RoomAggregate.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PublicId" }, "IDX_ROOM_PUBLICID")
                        .IsUnique();

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HyperChatApp.Core.Aggregates.RoomAggregate.RoomAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomAccess");
                });

            modelBuilder.Entity("HyperChatApp.Core.Aggregates.UserInfoAggregate.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthUserId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AuthUserId" }, "IDX_USERINFO_AUTHID")
                        .IsUnique();

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("HyperChatApp.Core.Aggregates.RoomAggregate.RoomAccess", b =>
                {
                    b.HasOne("HyperChatApp.Core.Aggregates.RoomAggregate.Room", null)
                        .WithMany("RoomAccesses")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("HyperChatApp.Core.Aggregates.RoomAggregate.Room", b =>
                {
                    b.Navigation("RoomAccesses");
                });
#pragma warning restore 612, 618
        }
    }
}
