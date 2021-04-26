using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class create_tables : Migration
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
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wallets_WalletTypes_WalletTypeId",
                        column: x => x.WalletTypeId,
                        principalTable: "WalletTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entraces_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("afe84d2d-3cfb-45f2-af1a-9cb1dcdead56"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5341), "Salary", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5347), null },
                    { new Guid("c7076130-71e9-4e6e-9279-998d487f60de"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5446), "Utilities", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5447), null },
                    { new Guid("fb156b5e-29f0-4b1f-a473-2d931efc3ee6"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5444), "Consumer Staples", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5445), null },
                    { new Guid("3f0f75d2-1504-429c-953a-2a6369c49375"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5442), "Health Care", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5443), null },
                    { new Guid("48b2a969-863e-4ee5-810c-489bf389182b"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5440), "Real Estate", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5441), null },
                    { new Guid("bcfe7168-9ccd-400d-b3eb-5744c695568f"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5438), "Communication Services", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5439), null },
                    { new Guid("245e6f7a-f849-4323-baee-dd1212c546af"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5435), "Information Technology", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5436), null },
                    { new Guid("6b89bf02-ecbd-4605-8a9a-0b1e3b28db62"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5433), "Consumer Discretionary", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5434), null },
                    { new Guid("b26c3953-e45e-4078-9c4f-e529aeb92648"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5427), "Financials", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5428), null },
                    { new Guid("c8735def-95a7-4600-b0e9-3a38796fe5a0"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5425), "Industrials", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5426), null },
                    { new Guid("2a01b945-1bf1-4da1-af03-51e85106e57c"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5423), "Gifts", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5424), null },
                    { new Guid("46ef3354-87ca-42cc-88ed-761531f39ee2"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5429), "Energy", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5430), null },
                    { new Guid("636cf9fe-ddc3-4b38-a080-6cc2f9dc1c3d"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5419), "Travel", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5420), null },
                    { new Guid("5a207b72-66b5-4621-b304-30cf1a6b472d"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5362), "Loans", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5363), null },
                    { new Guid("35603d7f-865f-4496-ac08-1276cacf8583"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5421), "Work", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5422), null },
                    { new Guid("3cf74692-98cc-4519-aff5-ea887aa4764c"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5367), "Investiments", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5367), null },
                    { new Guid("fdff15a0-efac-496f-8065-2ec89a72fae0"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5369), "Food", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5370), null },
                    { new Guid("19f0a471-e247-4c52-b7b5-6503561cef10"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5365), "Other Earnings", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5365), null },
                    { new Guid("fe0ff864-b8f0-4f2f-8a66-55e7a64ffa5d"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5375), "Services", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5376), null },
                    { new Guid("4b5da34a-5317-4c5c-af76-2dac83e67a41"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5413), "Health", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5414), null },
                    { new Guid("3c94fc52-f3b0-4013-80c3-ebf7ea0dd19c"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5416), "Education", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5417), null },
                    { new Guid("bd2abc15-5a49-4526-a8d2-3c6ca85c99da"), null, new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5373), "Transport", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(5374), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new DateTime(2021, 4, 26, 7, 59, 9, 693, DateTimeKind.Local).AddTicks(1851), "admin@admin.com", "Admin", "$2a$11$eS63Z.krrkXp971dszCK9.uR3tm3SYzxWIeyoS1/A/PDHW/DlXs.y", new DateTime(2021, 4, 26, 7, 59, 9, 693, DateTimeKind.Local).AddTicks(8917) },
                    { new Guid("cb43d078-87f1-4864-853a-e626922b8109"), new DateTime(2021, 4, 26, 7, 59, 9, 816, DateTimeKind.Local).AddTicks(9769), "testUser01@email.com", "Test-User-01", "$2a$11$v/2.Dd2yL7Rfp4zymjQsgubDASRihHvYVIITxM1zelywzaIcIMGVq", new DateTime(2021, 4, 26, 7, 59, 9, 816, DateTimeKind.Local).AddTicks(9791) }
                });

            migrationBuilder.InsertData(
                table: "WalletTypes",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e40"), new DateTime(2021, 4, 26, 7, 59, 9, 818, DateTimeKind.Local).AddTicks(4428), "Investiments", new DateTime(2021, 4, 26, 7, 59, 9, 818, DateTimeKind.Local).AddTicks(4430) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37"), new DateTime(2021, 4, 26, 7, 59, 9, 818, DateTimeKind.Local).AddTicks(3423), "Checking Account", new DateTime(2021, 4, 26, 7, 59, 9, 818, DateTimeKind.Local).AddTicks(3431) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38"), new DateTime(2021, 4, 26, 7, 59, 9, 818, DateTimeKind.Local).AddTicks(3809), "Credit", new DateTime(2021, 4, 26, 7, 59, 9, 818, DateTimeKind.Local).AddTicks(3812) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39"), new DateTime(2021, 4, 26, 7, 59, 9, 818, DateTimeKind.Local).AddTicks(4128), "Saving", new DateTime(2021, 4, 26, 7, 59, 9, 818, DateTimeKind.Local).AddTicks(4131) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e41"), new DateTime(2021, 4, 26, 7, 59, 9, 818, DateTimeKind.Local).AddTicks(4719), "Stocks", new DateTime(2021, 4, 26, 7, 59, 9, 818, DateTimeKind.Local).AddTicks(4721) }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("722b84da-1628-49af-b09b-b622271af6ec"), new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(178), new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(965), 500.0, "Main Account", new DateTime(2021, 5, 11, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(470), "Inter", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(969), new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37") });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("c5ee0354-48af-4bac-a752-ab99f3c747b3"), new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(1007), new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(1014), 500.0, "Credit Card Account", new DateTime(2021, 5, 11, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(1009), "Credit", new DateTime(2021, 4, 26, 7, 59, 9, 819, DateTimeKind.Local).AddTicks(1015), new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38") });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");

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
