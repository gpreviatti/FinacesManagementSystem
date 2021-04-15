using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class create_users_seeders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalletType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CurrentValue = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WalletTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallet_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wallet_WalletType_WalletTypeId",
                        column: x => x.WalletTypeId,
                        principalTable: "WalletType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entrace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrace_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entrace_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("dcfb5a27-37a4-4755-866c-7aaa589ab33b"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3606), "Salary", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3610), null },
                    { new Guid("b51fa26b-b6aa-434e-94ee-ffc88d794f3f"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3735), "Utilities", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3736), null },
                    { new Guid("3566f3e1-780a-4234-8648-86a6a6fdc8fe"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3733), "Consumer Staples", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3734), null },
                    { new Guid("ac3df7c9-791b-4285-8d65-19c86cdd1c03"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3731), "Health Care", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3732), null },
                    { new Guid("9f6b538c-d2b7-46ef-bdd0-122593da42c3"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3727), "Real Estate", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3728), null },
                    { new Guid("861fb2f2-dff6-4d2b-8c1d-c6d2dfef7fe8"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3725), "Communication Services", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3726), null },
                    { new Guid("26fa0a1f-7c4c-4648-8258-cecc43d20ede"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3722), "Information Technology", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3723), null },
                    { new Guid("a9d573c6-64ae-41d9-9cf4-7c57579f605b"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3718), "Energy", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3719), null },
                    { new Guid("69601097-053b-4185-8de4-96c540c09c7d"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3716), "Financials", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3717), null },
                    { new Guid("08a005bb-7326-4f67-9b6c-9d1fb3be59f5"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3714), "Industrials", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3715), null },
                    { new Guid("0b3dd7c7-b5f3-4956-af95-d2993797855d"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3712), "Gifts", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3713), null },
                    { new Guid("3903f7df-789d-4308-a534-fff96cabc069"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3720), "Consumer Discretionary", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3721), null },
                    { new Guid("df699c6b-7f81-4e53-abad-bab6536f1c6b"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3706), "Travel", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3707), null },
                    { new Guid("0ec84ffd-6d2f-40fb-be49-aca36c8d8287"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3708), "Work", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3709), null },
                    { new Guid("37da63e3-908c-439f-9612-0258631f37f9"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3629), "Other Earnings", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3630), null },
                    { new Guid("8282e02e-baeb-4fae-bdb4-499f21b2261c"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3642), "Investiments", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3643), null },
                    { new Guid("2a56a85c-1fe1-46ea-b79c-456c22e68612"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3644), "Food", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3645), null },
                    { new Guid("0a902991-c8c1-400b-af79-80d93123d1d3"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3626), "Loans", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3627), null },
                    { new Guid("aaafa2a4-61de-4e0e-bfc7-6c5824aff8ea"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3699), "Services", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3700), null },
                    { new Guid("de229014-5550-458c-8dbb-dadbf828ceb0"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3701), "Health", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3702), null },
                    { new Guid("d7982c1e-0c8a-4e3d-8feb-a90d0ffcd48c"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3703), "Education", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3704), null },
                    { new Guid("e2fc1e93-0640-492e-a644-fa0163c9467b"), new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3697), "Transport", new DateTime(2021, 4, 15, 11, 28, 9, 269, DateTimeKind.Local).AddTicks(3698), null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new DateTime(2021, 4, 15, 11, 28, 9, 134, DateTimeKind.Local).AddTicks(8845), "admin@admin.com", "Admin", "$2a$11$kLvYepUxV8Dooy/9KVzFueZwJAZC3.T6wgQXnQk/gUJlxV8uDh86.", new DateTime(2021, 4, 15, 11, 28, 9, 135, DateTimeKind.Local).AddTicks(7965) },
                    { new Guid("cb43d078-87f1-4864-853a-e626922b8109"), new DateTime(2021, 4, 15, 11, 28, 9, 266, DateTimeKind.Local).AddTicks(6640), "testUser01@email.com", "Test-User-01", "$2a$11$pDQDnIcl7p83zKihK8RnYOXDtWDB5DFeMU81nSzH2cK0JRDrJB6mq", new DateTime(2021, 4, 15, 11, 28, 9, 266, DateTimeKind.Local).AddTicks(6664) }
                });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[,]
                {
                    { new Guid("2d4561b2-0f67-4c96-b0df-e5d1968eaa89"), new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(8268), new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(8937), 500.0, "Main Account", new DateTime(2021, 4, 30, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(8564), "Inter", new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(8940), null, null },
                    { new Guid("1d9c851a-ff0b-46a0-bcb8-6ad43f4da0d0"), new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(8959), new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(8965), 500.0, "Credit Card Account", new DateTime(2021, 4, 30, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(8960), "Credit", new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(8966), null, null }
                });

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e40"), new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(3548), "Investiments", new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(3551) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37"), new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(2578), "Checking Account", new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(2590) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38"), new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(2951), "Credit", new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(2954) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39"), new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(3259), "Saving", new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(3262) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e41"), new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(3828), "Stocks", new DateTime(2021, 4, 15, 11, 28, 9, 268, DateTimeKind.Local).AddTicks(3831) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                table: "Category",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrace_CategoryId",
                table: "Entrace",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrace_WalletId",
                table: "Entrace",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_WalletTypeId",
                table: "Wallet",
                column: "WalletTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entrace");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WalletType");
        }
    }
}
