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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
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
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "UserWallet",
                columns: table => new
                {
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWallet", x => new { x.UsersId, x.WalletsId });
                    table.ForeignKey(
                        name: "FK_UserWallet_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWallet_Wallet_WalletsId",
                        column: x => x.WalletsId,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("fef753f9-890c-44db-83a5-33b7e0bfb900"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8045), "Salary", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8052), null },
                    { new Guid("2e280ab2-44cf-42b1-8788-ffa7be9b4f4c"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8181), "Utilities", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8182), null },
                    { new Guid("e4972ed9-31cb-44e1-8046-1b199441e9f9"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8178), "Consumer Staples", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8179), null },
                    { new Guid("6a872f1e-171d-4459-a2df-2388203902d4"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8175), "Health Care", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8175), null },
                    { new Guid("b25a35ff-c1e8-4bbd-8a8f-60f596f49ed6"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8172), "Real Estate", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8173), null },
                    { new Guid("947abafb-0d59-4a4e-b647-894bf2ddc070"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8170), "Communication Services", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8170), null },
                    { new Guid("617d2792-60d8-4c7a-b57b-e3b920c3e377"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8167), "Information Technology", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8167), null },
                    { new Guid("527246dc-e059-4c52-9a44-392bd17f3a95"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8164), "Consumer Discretionary", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8165), null },
                    { new Guid("abc77fce-3d6c-4428-b941-fb47c225c24f"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8160), "Financials", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8161), null },
                    { new Guid("a5084a1f-7b63-4930-a729-38b4d264386a"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8157), "Industrials", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8158), null },
                    { new Guid("e2e912a6-f520-43a6-b3a7-254096300c95"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8153), "Gifts", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8154), null },
                    { new Guid("d25756ca-dc34-4488-bab7-c9911194ba3e"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8162), "Energy", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8163), null },
                    { new Guid("285cc3c3-348e-459f-b121-82fbf689e7ab"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8144), "Travel", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8144), null },
                    { new Guid("c46dc0e6-9985-4536-a57f-62c54e94d1cf"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8074), "Loans", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8075), null },
                    { new Guid("e72eb9a5-e5a3-4932-a609-43952e3de310"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8150), "Work", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8151), null },
                    { new Guid("ac4e93a6-2130-433c-b734-ed2f4d14602d"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8079), "Investiments", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8080), null },
                    { new Guid("b74d7ba6-761c-49f1-9f0b-88cdc30ecfab"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8094), "Food", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8108), null },
                    { new Guid("3918e169-6739-43b2-87ea-d16d9fa3fa6e"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8077), "Other Earnings", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8078), null },
                    { new Guid("7bbc4c38-fade-4f48-864b-93a6aeadcbeb"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8135), "Services", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8136), null },
                    { new Guid("202f3c6d-b252-41ea-b795-0913eac4c074"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8138), "Health", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8139), null },
                    { new Guid("364fd8c6-e1cc-4bfa-ba8b-358284a2c672"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8140), "Education", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8141), null },
                    { new Guid("8beb0f1a-c208-416e-9277-f418ea947801"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8133), "Transport", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(8134), null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new DateTime(2021, 4, 14, 18, 48, 49, 579, DateTimeKind.Local).AddTicks(724), "admin@admin.com", "Admin", "$2a$11$fv.zL0RZ2jKqQXqjsAxf7OZLLLbjEXNfldSunnebhFNbYIiWkx6bG", new DateTime(2021, 4, 14, 18, 48, 49, 579, DateTimeKind.Local).AddTicks(9529) },
                    { new Guid("cb43d078-87f1-4864-853a-e626922b8109"), new DateTime(2021, 4, 14, 18, 48, 49, 737, DateTimeKind.Local).AddTicks(8217), "testUser01@email.com", "Test-User-01", "$2a$11$mkdnKEMQh5WtSsL0iDnC6.kgxflQong1dMCYKRhohCqJVXr84faw2", new DateTime(2021, 4, 14, 18, 48, 49, 737, DateTimeKind.Local).AddTicks(8247) }
                });

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e40"), new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(3644), "Investiments", new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(3648) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37"), new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(1121), "Checking Account", new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(1284) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38"), new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(2364), "Credit", new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(2380) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39"), new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(3237), "Saving", new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(3257) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e41"), new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(4104), "Stocks", new DateTime(2021, 4, 14, 18, 48, 49, 740, DateTimeKind.Local).AddTicks(4108) }
                });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("8608269e-1d91-4ce9-9059-5872c9297eb1"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(1276), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2421), 500.0, "Main Account", new DateTime(2021, 4, 29, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(1638), "Inter", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2426), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37") });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("feac5d9a-43cc-4756-a359-68671af52c8c"), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2466), new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2473), 500.0, "Credit Card Account", new DateTime(2021, 4, 29, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2467), "Credit", new DateTime(2021, 4, 14, 18, 48, 49, 741, DateTimeKind.Local).AddTicks(2474), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38") });

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
                name: "IX_UserWallet_WalletsId",
                table: "UserWallet",
                column: "WalletsId");

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
                name: "UserWallet");

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
