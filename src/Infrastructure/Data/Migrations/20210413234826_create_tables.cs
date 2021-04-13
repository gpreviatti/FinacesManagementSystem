using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class create_tables : Migration
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
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    { new Guid("f804820a-2e8e-48ce-b589-27959490fe3b"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5590), "Salary", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5610), null },
                    { new Guid("3c53308d-a039-4e14-8ce6-8a609c6612af"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5900), "Utilities", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5902), null },
                    { new Guid("a83decb6-5edf-4b9b-8aae-040eb255195d"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5893), "Consumer Staples", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5895), null },
                    { new Guid("c9b23c6a-4840-4bcf-960b-0af5e2cc4a56"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5879), "Health Care", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5881), null },
                    { new Guid("46669da6-1f4f-4a8a-b4f7-04328c720587"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5872), "Real Estate", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5874), null },
                    { new Guid("995475e2-cb22-4c74-ad2e-c83dbaadd219"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5830), "Communication Services", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5860), null },
                    { new Guid("00b780e5-a23a-4a3d-b8d0-49725437306a"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5820), "Information Technology", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5822), null },
                    { new Guid("9165e84b-8b53-45af-a580-2fae686a7550"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5814), "Consumer Discretionary", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5816), null },
                    { new Guid("6fe5ef3c-2d01-4e83-9fbf-98aff9c2d79d"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5799), "Financials", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5801), null },
                    { new Guid("a71029a7-3f43-4378-a10c-40486e66f031"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5792), "Industrials", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5794), null },
                    { new Guid("1028b729-7145-41cd-b70c-d27c4de74213"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5781), "Gifts", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5783), null },
                    { new Guid("c18933e8-6166-4be5-b4f2-341b70fd459e"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5806), "Energy", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5808), null },
                    { new Guid("8122d82c-7d2f-486d-86b4-311ee208b776"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5769), "Travel", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5771), null },
                    { new Guid("e6cfad65-bba4-4262-ac28-3592835b9bd9"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5661), "Loans", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5662), null },
                    { new Guid("8693c687-02dc-4769-a2e2-00e80d1fdb72"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5775), "Work", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5777), null },
                    { new Guid("02a33a98-ae20-4cab-8ce9-bb75c0287582"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5671), "Investiments", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5672), null },
                    { new Guid("a0788543-9f2d-481e-8db2-09c713507a04"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5689), "Food", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5691), null },
                    { new Guid("978c7423-78ae-4e16-bb1e-3d9604ac68e9"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5666), "Other Earnings", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5667), null },
                    { new Guid("7f5c1293-7f9f-409b-9d8c-be8d8187a6f0"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5721), "Services", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5731), null },
                    { new Guid("a3089937-546e-4a3c-ac8e-6d34228d7455"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5753), "Health", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5755), null },
                    { new Guid("673dfe69-fe8a-4d9c-9ec0-3173021f62cb"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5758), "Education", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5760), null },
                    { new Guid("3ebb3280-93e1-4e43-b306-398eba1c4ac5"), new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5704), "Transport", new DateTime(2021, 4, 13, 20, 48, 24, 451, DateTimeKind.Local).AddTicks(5717), null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new DateTime(2021, 4, 13, 20, 48, 24, 211, DateTimeKind.Local).AddTicks(2245), "admin@admin.com", "Admin", "$2a$11$ia3SGHFexYRAYWYgA5CWQuM6EDeEH9zWaB/Vc.k8xrKw1TM92.85e", new DateTime(2021, 4, 13, 20, 48, 24, 212, DateTimeKind.Local).AddTicks(4624) },
                    { new Guid("cb43d078-87f1-4864-853a-e626922b8109"), new DateTime(2021, 4, 13, 20, 48, 24, 447, DateTimeKind.Local).AddTicks(6397), "testUser01@email.com", "Test-User-01", "$2a$11$7KYrNZXtckfCyrDFA8UgguMbCqUICDltEgJNcVF0QmeccTZzvrFFu", new DateTime(2021, 4, 13, 20, 48, 24, 447, DateTimeKind.Local).AddTicks(6432) }
                });

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e40"), new DateTime(2021, 4, 13, 20, 48, 24, 449, DateTimeKind.Local).AddTicks(6981), "Investiments", new DateTime(2021, 4, 13, 20, 48, 24, 449, DateTimeKind.Local).AddTicks(6983) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37"), new DateTime(2021, 4, 13, 20, 48, 24, 449, DateTimeKind.Local).AddTicks(6909), "Checking Account", new DateTime(2021, 4, 13, 20, 48, 24, 449, DateTimeKind.Local).AddTicks(6931) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38"), new DateTime(2021, 4, 13, 20, 48, 24, 449, DateTimeKind.Local).AddTicks(6970), "Credit", new DateTime(2021, 4, 13, 20, 48, 24, 449, DateTimeKind.Local).AddTicks(6971) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39"), new DateTime(2021, 4, 13, 20, 48, 24, 449, DateTimeKind.Local).AddTicks(6976), "Saving", new DateTime(2021, 4, 13, 20, 48, 24, 449, DateTimeKind.Local).AddTicks(6977) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e41"), new DateTime(2021, 4, 13, 20, 48, 24, 449, DateTimeKind.Local).AddTicks(6993), "Stocks", new DateTime(2021, 4, 13, 20, 48, 24, 449, DateTimeKind.Local).AddTicks(6995) }
                });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("dc48ab84-505d-4095-a0c4-a0616809f570"), new DateTime(2021, 4, 13, 20, 48, 24, 450, DateTimeKind.Local).AddTicks(5049), new DateTime(2021, 4, 13, 20, 48, 24, 450, DateTimeKind.Local).AddTicks(6961), 500.0, "Main Account", new DateTime(2021, 4, 28, 20, 48, 24, 450, DateTimeKind.Local).AddTicks(5657), "Inter", new DateTime(2021, 4, 13, 20, 48, 24, 450, DateTimeKind.Local).AddTicks(6968), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37") });

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
