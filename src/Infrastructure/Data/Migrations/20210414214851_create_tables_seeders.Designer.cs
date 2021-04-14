﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210414214851_create_tables_seeders")]
    partial class create_tables_seeders
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fef753f9-890c-44db-83a5-33b7e0bfb900"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8045),
                            Name = "Salary",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8052)
                        },
                        new
                        {
                            Id = new Guid("c46dc0e6-9985-4536-a57f-62c54e94d1cf"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8074),
                            Name = "Loans",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8075)
                        },
                        new
                        {
                            Id = new Guid("3918e169-6739-43b2-87ea-d16d9fa3fa6e"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8077),
                            Name = "Other Earnings",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8078)
                        },
                        new
                        {
                            Id = new Guid("ac4e93a6-2130-433c-b734-ed2f4d14602d"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8079),
                            Name = "Investiments",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8080)
                        },
                        new
                        {
                            Id = new Guid("b74d7ba6-761c-49f1-9f0b-88cdc30ecfab"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8094),
                            Name = "Food",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8108)
                        },
                        new
                        {
                            Id = new Guid("8beb0f1a-c208-416e-9277-f418ea947801"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8133),
                            Name = "Transport",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8134)
                        },
                        new
                        {
                            Id = new Guid("7bbc4c38-fade-4f48-864b-93a6aeadcbeb"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8135),
                            Name = "Services",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8136)
                        },
                        new
                        {
                            Id = new Guid("202f3c6d-b252-41ea-b795-0913eac4c074"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8138),
                            Name = "Health",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8139)
                        },
                        new
                        {
                            Id = new Guid("364fd8c6-e1cc-4bfa-ba8b-358284a2c672"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8140),
                            Name = "Education",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8141)
                        },
                        new
                        {
                            Id = new Guid("285cc3c3-348e-459f-b121-82fbf689e7ab"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8144),
                            Name = "Travel",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8144)
                        },
                        new
                        {
                            Id = new Guid("e72eb9a5-e5a3-4932-a609-43952e3de310"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8150),
                            Name = "Work",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8151)
                        },
                        new
                        {
                            Id = new Guid("e2e912a6-f520-43a6-b3a7-254096300c95"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8153),
                            Name = "Gifts",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8154)
                        },
                        new
                        {
                            Id = new Guid("a5084a1f-7b63-4930-a729-38b4d264386a"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8157),
                            Name = "Industrials",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8158)
                        },
                        new
                        {
                            Id = new Guid("abc77fce-3d6c-4428-b941-fb47c225c24f"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8160),
                            Name = "Financials",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8161)
                        },
                        new
                        {
                            Id = new Guid("d25756ca-dc34-4488-bab7-c9911194ba3e"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8162),
                            Name = "Energy",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8163)
                        },
                        new
                        {
                            Id = new Guid("527246dc-e059-4c52-9a44-392bd17f3a95"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8164),
                            Name = "Consumer Discretionary",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8165)
                        },
                        new
                        {
                            Id = new Guid("617d2792-60d8-4c7a-b57b-e3b920c3e377"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8167),
                            Name = "Information Technology",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8167)
                        },
                        new
                        {
                            Id = new Guid("947abafb-0d59-4a4e-b647-894bf2ddc070"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8170),
                            Name = "Communication Services",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8170)
                        },
                        new
                        {
                            Id = new Guid("b25a35ff-c1e8-4bbd-8a8f-60f596f49ed6"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8172),
                            Name = "Real Estate",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8173)
                        },
                        new
                        {
                            Id = new Guid("6a872f1e-171d-4459-a2df-2388203902d4"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8175),
                            Name = "Health Care",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8175)
                        },
                        new
                        {
                            Id = new Guid("e4972ed9-31cb-44e1-8046-1b199441e9f9"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8178),
                            Name = "Consumer Staples",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8179)
                        },
                        new
                        {
                            Id = new Guid("2e280ab2-44cf-42b1-8788-ffa7be9b4f4c"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8181),
                            Name = "Utilities",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8182)
                        });
                });

            modelBuilder.Entity("Domain.Entities.Entrace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Observation")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Ticker")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("WalletId");

                    b.ToTable("Entrace");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 579, DateTimeKind.Local).AddTicks(724),
                            Email = "admin@admin.com",
                            Name = "Admin",
                            Password = "$2a$11$fv.zL0RZ2jKqQXqjsAxf7OZLLLbjEXNfldSunnebhFNbYIiWkx6bG",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 579, DateTimeKind.Local).AddTicks(9529)
                        },
                        new
                        {
                            Id = new Guid("cb43d078-87f1-4864-853a-e626922b8109"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 737, DateTimeKind.Local).AddTicks(8217),
                            Email = "testUser01@email.com",
                            Name = "Test-User-01",
                            Password = "$2a$11$mkdnKEMQh5WtSsL0iDnC6.kgxflQong1dMCYKRhohCqJVXr84faw2",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 737, DateTimeKind.Local).AddTicks(8247)
                        });
                });

            modelBuilder.Entity("Domain.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CloseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("CurrentValue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WalletTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WalletTypeId");

                    b.ToTable("Wallet");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8608269e-1d91-4ce9-9059-5872c9297eb1"),
                            CloseDate = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(1276),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2421),
                            CurrentValue = 500.0,
                            Description = "Main Account",
                            DueDate = new DateTime(2021, 4, 29, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(1638),
                            Name = "Inter",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2426),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000"),
                            WalletTypeId = new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37")
                        },
                        new
                        {
                            Id = new Guid("feac5d9a-43cc-4756-a359-68671af52c8c"),
                            CloseDate = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2466),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2473),
                            CurrentValue = 500.0,
                            Description = "Credit Card Account",
                            DueDate = new DateTime(2021, 4, 29, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2467),
                            Name = "Credit",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2474),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000"),
                            WalletTypeId = new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38")
                        });
                });

            modelBuilder.Entity("Domain.Entities.WalletType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("WalletType");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(1121),
                            Name = "Checking Account",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(1284)
                        },
                        new
                        {
                            Id = new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(2364),
                            Name = "Credit",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(2380)
                        },
                        new
                        {
                            Id = new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(3237),
                            Name = "Saving",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(3257)
                        },
                        new
                        {
                            Id = new Guid("ad4ac47f-0888-4d60-81f9-964153b13e40"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(3644),
                            Name = "Investiments",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(3648)
                        },
                        new
                        {
                            Id = new Guid("ad4ac47f-0888-4d60-81f9-964153b13e41"),
                            CreatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(4104),
                            Name = "Stocks",
                            UpdatedAt = new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(4108)
                        });
                });

            modelBuilder.Entity("UserWallet", b =>
                {
                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WalletsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UsersId", "WalletsId");

                    b.HasIndex("WalletsId");

                    b.ToTable("UserWallet");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany("Categories")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Domain.Entities.Entrace", b =>
                {
                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Wallet", "Wallet")
                        .WithMany("Entraces")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Domain.Entities.Wallet", b =>
                {
                    b.HasOne("Domain.Entities.WalletType", "WalletType")
                        .WithMany()
                        .HasForeignKey("WalletTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WalletType");
                });

            modelBuilder.Entity("UserWallet", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Wallet", null)
                        .WithMany()
                        .HasForeignKey("WalletsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Domain.Entities.Wallet", b =>
                {
                    b.Navigation("Entraces");
                });
#pragma warning restore 612, 618
        }
    }
}