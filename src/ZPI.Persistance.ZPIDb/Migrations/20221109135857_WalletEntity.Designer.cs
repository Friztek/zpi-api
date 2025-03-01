﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ZPI.Persistance.ZPIDb;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    [DbContext(typeof(ZPIDbContext))]
    [Migration("20221109135857_WalletEntity")]
    partial class WalletEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("zpi")
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ZPI.Persistance.Entities.AlertEntity", b =>
                {
                    b.Property<int>("Identifier")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("OriginAssetId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TargetCurrency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Identifier", "UserId");

                    b.HasIndex("OriginAssetId");

                    b.ToTable("Alerts", "zpi");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.AssetEntity", b =>
                {
                    b.Property<string>("Identifier")
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FriendlyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Identifier");

                    b.ToTable("Assets", "zpi");

                    b.HasData(
                        new
                        {
                            Identifier = "btc",
                            Category = "crypto",
                            FriendlyName = "Bitcoin"
                        },
                        new
                        {
                            Identifier = "gold",
                            Category = "metal",
                            FriendlyName = "Gold"
                        },
                        new
                        {
                            Identifier = "silver",
                            Category = "metal",
                            FriendlyName = "Silver"
                        },
                        new
                        {
                            Identifier = "platinum",
                            Category = "metal",
                            FriendlyName = "Platinum"
                        },
                        new
                        {
                            Identifier = "eth",
                            Category = "crypto",
                            FriendlyName = "Ethereum"
                        },
                        new
                        {
                            Identifier = "ltc",
                            Category = "crypto",
                            FriendlyName = "Litecoin"
                        },
                        new
                        {
                            Identifier = "eur",
                            Category = "currency",
                            FriendlyName = "Euro",
                            Symbol = "€"
                        },
                        new
                        {
                            Identifier = "pln",
                            Category = "currency",
                            FriendlyName = "Polish złoty",
                            Symbol = "zł"
                        },
                        new
                        {
                            Identifier = "jpy",
                            Category = "currency",
                            FriendlyName = "Japanese Yen",
                            Symbol = "¥"
                        },
                        new
                        {
                            Identifier = "gbp",
                            Category = "currency",
                            FriendlyName = "Pound sterling",
                            Symbol = "£"
                        },
                        new
                        {
                            Identifier = "huf",
                            Category = "currency",
                            FriendlyName = "Hungarian Forint",
                            Symbol = "Ft"
                        },
                        new
                        {
                            Identifier = "try",
                            Category = "currency",
                            FriendlyName = "Turkish lira",
                            Symbol = "₺"
                        },
                        new
                        {
                            Identifier = "sek",
                            Category = "currency",
                            FriendlyName = "Swedish Krona",
                            Symbol = "kr"
                        },
                        new
                        {
                            Identifier = "chf",
                            Category = "currency",
                            FriendlyName = "Swiss Franc",
                            Symbol = "CHf"
                        },
                        new
                        {
                            Identifier = "rub",
                            Category = "currency",
                            FriendlyName = "Russian Ruble",
                            Symbol = "₽"
                        },
                        new
                        {
                            Identifier = "nok",
                            Category = "currency",
                            FriendlyName = "Norwegian Krone",
                            Symbol = "kr"
                        },
                        new
                        {
                            Identifier = "cad",
                            Category = "currency",
                            FriendlyName = "Canadian Dollar",
                            Symbol = "$"
                        },
                        new
                        {
                            Identifier = "inr",
                            Category = "currency",
                            FriendlyName = "Indian Rupee",
                            Symbol = "₹"
                        },
                        new
                        {
                            Identifier = "czk",
                            Category = "currency",
                            FriendlyName = "Czech Koruna",
                            Symbol = "Kč"
                        },
                        new
                        {
                            Identifier = "hrk",
                            Category = "currency",
                            FriendlyName = "Croatian Kuna",
                            Symbol = "kn"
                        },
                        new
                        {
                            Identifier = "usd",
                            Category = "currency",
                            FriendlyName = "United States Dollar",
                            Symbol = "$"
                        });
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.AssetValueAtDay", b =>
                {
                    b.Property<string>("AssetIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<OffsetDateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.ToView("AssetValueAtDay");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.AssetValueAtm", b =>
                {
                    b.Property<string>("AssetIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<OffsetDateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.ToView("AssetValueAtm");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.AssetValueEntity", b =>
                {
                    b.Property<long>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Identifier"));

                    b.Property<string>("AssetIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<OffsetDateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Identifier");

                    b.HasIndex("AssetIdentifier");

                    b.ToTable("AssetValues", "zpi");

                    b.HasData(
                        new
                        {
                            Identifier = -1L,
                            AssetIdentifier = "usd",
                            TimeStamp = new NodaTime.OffsetDateTime(new NodaTime.LocalDateTime(1, 1, 1, 0, 0), NodaTime.Offset.FromHours(0)),
                            Value = 1.0
                        });
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.TransactionEntity", b =>
                {
                    b.Property<long>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Identifier"));

                    b.Property<string>("AssetIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<OffsetDateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Identifier");

                    b.HasIndex("AssetIdentifier");

                    b.ToTable("TransactionEntity", "zpi");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.UserAssetEntity", b =>
                {
                    b.Property<string>("AssetIdentifier")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("AssetIdentifier", "UserId");

                    b.ToTable("UserAssets", "zpi");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.UserPreferencesEntity", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<bool>("AlertsOnEmail")
                        .HasColumnType("boolean");

                    b.Property<string>("PreferenceCurrency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("WeeklyReports")
                        .HasColumnType("boolean");

                    b.HasKey("UserId");

                    b.ToTable("UserPreferences", "zpi");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.WalletEntity", b =>
                {
                    b.Property<long>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Identifier"));

                    b.Property<OffsetDateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Identifier");

                    b.ToTable("WalletEntity", "zpi");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.AlertEntity", b =>
                {
                    b.HasOne("ZPI.Persistance.Entities.AssetEntity", "Asset")
                        .WithMany("Alerts")
                        .HasForeignKey("OriginAssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.AssetValueEntity", b =>
                {
                    b.HasOne("ZPI.Persistance.Entities.AssetEntity", "Asset")
                        .WithMany("Values")
                        .HasForeignKey("AssetIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.TransactionEntity", b =>
                {
                    b.HasOne("ZPI.Persistance.Entities.AssetEntity", "Asset")
                        .WithMany("Transactions")
                        .HasForeignKey("AssetIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.UserAssetEntity", b =>
                {
                    b.HasOne("ZPI.Persistance.Entities.AssetEntity", "Asset")
                        .WithMany("UserAssets")
                        .HasForeignKey("AssetIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("ZPI.Persistance.Entities.AssetEntity", b =>
                {
                    b.Navigation("Alerts");

                    b.Navigation("Transactions");

                    b.Navigation("UserAssets");

                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}
