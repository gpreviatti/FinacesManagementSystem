using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class create_tables_seeders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalletTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wallets_WalletTypes_WalletTypeId",
                        column: x => x.WalletTypeId,
                        principalTable: "WalletTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entraces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entraces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entraces_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entraces_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("d2da5a0d-4e39-4dcf-bce0-d8e8981ff00b"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6668), "Salary", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6673), null },
                    { new Guid("cb85e762-1b4d-476d-a1dc-93e0600686e8"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6751), "Utilities", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6752), null },
                    { new Guid("ce1cdadb-9815-4fc8-ae8f-24a058061485"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6749), "Consumer Staples", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6750), null },
                    { new Guid("1e841727-59fb-46a6-9cd9-55d0dfc3d4cc"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6747), "Health Care", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6747), null },
                    { new Guid("4aadf1ce-8506-4aba-9548-a05c7ce80868"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6743), "Real Estate", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6744), null },
                    { new Guid("94531a8b-e6be-47bb-bc7b-3adc4dde6477"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6741), "Communication Services", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6742), null },
                    { new Guid("a63a4987-b4bf-4025-ad1b-0d61626267e0"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6738), "Information Technology", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6739), null },
                    { new Guid("eb4f5e89-8743-4e77-bb05-cf293ed7f207"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6734), "Energy", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6735), null },
                    { new Guid("2dd31216-134d-4737-81df-e388349bc2f8"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6732), "Financials", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6733), null },
                    { new Guid("a43165fa-8403-42ed-ab4b-c1686959d834"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6730), "Industrials", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6731), null },
                    { new Guid("7e5e1056-3da8-4e20-9f41-cfd58a1bab4e"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6728), "Gifts", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6729), null },
                    { new Guid("f4eff17e-a76e-45ee-843f-f18b00a62b5d"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6736), "Consumer Discretionary", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6737), null },
                    { new Guid("0eb72c2e-00c7-45a7-b478-c2215df7546d"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6722), "Travel", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6723), null },
                    { new Guid("771a1988-82dc-46ef-baab-815516a5993e"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6724), "Work", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6725), null },
                    { new Guid("e68fc136-5351-4ec5-ad4f-75a670ae8a76"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6694), "Other Earnings", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6695), null },
                    { new Guid("41ccdcd6-44cb-4968-ba0a-fb120af5a9ed"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6706), "Investiments", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6707), null },
                    { new Guid("ead01912-ea50-46ea-9755-4a484f2b00ce"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6708), "Food", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6709), null },
                    { new Guid("9a05a596-37a6-4405-abdc-2923108df5b9"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6691), "Loans", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6692), null },
                    { new Guid("693d4c02-ba7d-4cd7-b065-87c410bda335"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6715), "Services", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6715), null },
                    { new Guid("5fa730e2-3d56-4f93-bc73-578eacbd80ad"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6717), "Health", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6718), null },
                    { new Guid("908ea658-beb9-40ef-8385-e1879859b989"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6719), "Education", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6720), null },
                    { new Guid("954e872b-4b54-4bbc-b616-805d47891ce4"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6713), "Transport", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(6713), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new DateTime(2021, 4, 16, 10, 36, 12, 755, DateTimeKind.Local).AddTicks(1294), "admin@admin.com", "Admin", "$2a$11$0W.jIZlK4WSgGPrsyCFQIeXo8m1RNEqC6R42rVlffpKGhwgVmdzKS", new DateTime(2021, 4, 16, 10, 36, 12, 755, DateTimeKind.Local).AddTicks(9568) },
                    { new Guid("cb43d078-87f1-4864-853a-e626922b8109"), new DateTime(2021, 4, 16, 10, 36, 12, 884, DateTimeKind.Local).AddTicks(293), "testUser01@email.com", "Test-User-01", "$2a$11$cbFXzbtBafCVHDCwy7eOj.hXYpjpeb7cObKBOOXu7DfzSRVX94Hw6", new DateTime(2021, 4, 16, 10, 36, 12, 884, DateTimeKind.Local).AddTicks(319) }
                });

            migrationBuilder.InsertData(
                table: "WalletTypes",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e40"), new DateTime(2021, 4, 16, 10, 36, 12, 885, DateTimeKind.Local).AddTicks(6950), "Investiments", new DateTime(2021, 4, 16, 10, 36, 12, 885, DateTimeKind.Local).AddTicks(6952) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37"), new DateTime(2021, 4, 16, 10, 36, 12, 885, DateTimeKind.Local).AddTicks(5987), "Checking Account", new DateTime(2021, 4, 16, 10, 36, 12, 885, DateTimeKind.Local).AddTicks(5996) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38"), new DateTime(2021, 4, 16, 10, 36, 12, 885, DateTimeKind.Local).AddTicks(6352), "Credit", new DateTime(2021, 4, 16, 10, 36, 12, 885, DateTimeKind.Local).AddTicks(6356) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39"), new DateTime(2021, 4, 16, 10, 36, 12, 885, DateTimeKind.Local).AddTicks(6661), "Saving", new DateTime(2021, 4, 16, 10, 36, 12, 885, DateTimeKind.Local).AddTicks(6664) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e41"), new DateTime(2021, 4, 16, 10, 36, 12, 885, DateTimeKind.Local).AddTicks(7252), "Stocks", new DateTime(2021, 4, 16, 10, 36, 12, 885, DateTimeKind.Local).AddTicks(7254) }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[,]
                {
                    { new Guid("00a418bd-7649-4997-ad8f-915c952899c0"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(1725), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(2373), 500.0, "Main Account", new DateTime(2021, 5, 1, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(2016), "Inter", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(2376), null, null },
                    { new Guid("8ddfdfa9-0fd7-4d03-bfb7-b715622e9891"), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(2395), new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(2401), 500.0, "Credit Card Account", new DateTime(2021, 5, 1, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(2396), "Credit", new DateTime(2021, 4, 16, 10, 36, 12, 886, DateTimeKind.Local).AddTicks(2402), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entraces_CategoryId",
                table: "Entraces",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Entraces_WalletId",
                table: "Entraces",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_WalletTypeId",
                table: "Wallets",
                column: "WalletTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entraces");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WalletTypes");
        }
    }
}
