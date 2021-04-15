using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class create_tables_seeders : Migration
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wallet_WalletType_WalletTypeId",
                        column: x => x.WalletTypeId,
                        principalTable: "WalletType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entrace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entrace_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("0d7dbf5e-4009-499f-b589-b0d7ceb0910f"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5074), "Salary", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5078), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("738f7b4a-dae7-43f8-bf7f-3b243afad497"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5146), "Utilities", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5146), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f958b89a-d094-4183-a5f9-46e5e86b7f87"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5143), "Consumer Staples", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5144), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e866518f-b231-4d89-ae7f-dfb9e8326167"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5141), "Health Care", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5142), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("1265c47c-3ff4-43a3-8e85-e1c6f6396866"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5139), "Real Estate", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5140), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ae8f94ba-2342-42ac-b2a6-1da18904816d"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5137), "Communication Services", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("495b8d84-644d-4422-bb14-9ac165f5e435"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5134), "Information Technology", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5135), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c44eb8f4-cc0b-4da9-aa1c-0e40eef337cc"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5131), "Consumer Discretionary", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5131), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d7a37597-f9a9-4b17-a546-9b06f6e48212"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5127), "Financials", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5127), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f881a742-9fbe-4a4d-aef3-21a9af7576c7"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5124), "Industrials", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5125), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("52a1be5f-345b-4d0b-986d-8e24583e829a"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5122), "Gifts", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5123), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9505b4e1-1851-4609-bbe4-f784c49e0c16"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5129), "Energy", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5129), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("75d8a548-517d-4379-b303-b54dc4867e09"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5118), "Travel", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5119), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("3162d7eb-0965-4725-bf19-c5e7a5b8eb4a"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5094), "Loans", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5095), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d774576f-6692-402a-8d8a-af949d1a8f15"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5120), "Work", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5121), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("626fd70e-f5d5-43a1-99ff-6bf486c9351f"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5099), "Investiments", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5100), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c7d07c09-3540-4406-9485-eaf61ca48f1b"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5101), "Food", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5102), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("95ace88d-8237-4c32-bee9-650ee9a5cf34"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5097), "Other Earnings", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5098), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("471ebcc8-9af6-4379-8c46-d21724a665f4"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5108), "Services", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5109), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("1c67f047-9043-4c63-af03-c179bd2714b4"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5110), "Health", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5111), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e658b1fc-4b52-489d-a6b5-d21449519f58"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5115), "Education", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5116), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("64330374-60b0-4eee-b8de-439b6f1af7c2"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5106), "Transport", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(5106), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new DateTime(2021, 4, 14, 21, 52, 46, 449, DateTimeKind.Local).AddTicks(2020), "admin@admin.com", "Admin", "$2a$11$p/DG0Ns5IKvaQunbEDtdPO6exntYhOQTzNaqyqObVz8kZFAI79ts6", new DateTime(2021, 4, 14, 21, 52, 46, 450, DateTimeKind.Local).AddTicks(100) },
                    { new Guid("cb43d078-87f1-4864-853a-e626922b8109"), new DateTime(2021, 4, 14, 21, 52, 46, 579, DateTimeKind.Local).AddTicks(5918), "testUser01@email.com", "Test-User-01", "$2a$11$o6FUlQTStiQqwCCcYjz9L.f/NEnISVt8J/8WiL900ArA60vpArtZK", new DateTime(2021, 4, 14, 21, 52, 46, 579, DateTimeKind.Local).AddTicks(5938) }
                });

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e40"), new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(3603), "Investiments", new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(3605) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37"), new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(1928), "Checking Account", new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(1979) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38"), new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(2663), "Credit", new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(2665) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39"), new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(2975), "Saving", new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(2978) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e41"), new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(3887), "Stocks", new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(3889) }
                });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("bba1811c-2384-49dc-950c-3418219c65fe"), new DateTime(2021, 4, 14, 21, 52, 46, 581, DateTimeKind.Local).AddTicks(9730), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(666), 500.0, "Main Account", new DateTime(2021, 4, 29, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(27), "Inter", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(670), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37") });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("bfa4b9cb-417f-45fd-82b0-039b1582e0ea"), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(692), new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(701), 500.0, "Credit Card Account", new DateTime(2021, 4, 29, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(693), "Credit", new DateTime(2021, 4, 14, 21, 52, 46, 582, DateTimeKind.Local).AddTicks(701), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38") });

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
