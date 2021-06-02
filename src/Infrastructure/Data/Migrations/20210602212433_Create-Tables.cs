using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 172, DateTimeKind.Local).AddTicks(6436)),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Propperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 144, DateTimeKind.Local).AddTicks(7345)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 152, DateTimeKind.Local).AddTicks(8709))
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 162, DateTimeKind.Local).AddTicks(938)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 162, DateTimeKind.Local).AddTicks(1362))
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 165, DateTimeKind.Local).AddTicks(4695)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 165, DateTimeKind.Local).AddTicks(5011))
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 163, DateTimeKind.Local).AddTicks(2682)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 163, DateTimeKind.Local).AddTicks(3566))
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
                name: "Entrances",
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 169, DateTimeKind.Local).AddTicks(4737)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 6, 2, 18, 24, 33, 169, DateTimeKind.Local).AddTicks(5098))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrances_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entrances_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryId", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("18301de4-a10f-404e-8ce7-836f297382bb"), null, "Salary", null },
                    { new Guid("18461de4-a10f-404e-8ce7-836f297382bb"), null, "Other Expanses", null },
                    { new Guid("18451de4-a10f-404e-8ce7-836f297382bb"), null, "Home", null },
                    { new Guid("18441de4-a10f-404e-8ce7-836f297382bb"), null, "Gifts", null },
                    { new Guid("18421de4-a10f-404e-8ce7-836f297382bb"), null, "Travel", null },
                    { new Guid("18411de4-a10f-404e-8ce7-836f297382bb"), null, "Education", null },
                    { new Guid("18401de4-a10f-404e-8ce7-836f297382bb"), null, "Health", null },
                    { new Guid("18431de4-a10f-404e-8ce7-836f297382bb"), null, "Work", null },
                    { new Guid("18381de4-a10f-404e-8ce7-836f297382bb"), null, "Transport", null },
                    { new Guid("18371de4-a10f-404e-8ce7-836f297382bb"), null, "Food", null },
                    { new Guid("18361de4-a10f-404e-8ce7-836f297382bb"), null, "Investiments", null },
                    { new Guid("18351de4-a10f-404e-8ce7-836f297382bb"), null, "Other Earnings", null },
                    { new Guid("18341de4-a10f-404e-8ce7-836f297382bb"), null, "Loans", null },
                    { new Guid("18391de4-a10f-404e-8ce7-836f297382bb"), null, "Services", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), "admin@admin.com", "Admin", "$2a$11$k8al7XOOgrZwu25tOY30POrA75n7XXHJlozGDK1TtwDXjp0IrsQQ6" },
                    { new Guid("cb43d078-87f1-4864-853a-e626922b8109"), "testUser01@email.com", "Test-User-01", "$2a$11$Dxf4l/m622igWPFwxyPvxOGtW77NlipYXh.E4V0CEw4nmrP/4lFZK" }
                });

            migrationBuilder.InsertData(
                table: "WalletTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e40"), "Investiments" },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37"), "Checking Account" },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38"), "Credit" },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39"), "Saving" },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e41"), "Stocks" }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CurrentValue", "Description", "DueDate", "Name", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("040cc3ad-2159-4b8e-894e-e700a121b48f"), new DateTime(2021, 6, 2, 18, 24, 33, 443, DateTimeKind.Local).AddTicks(6918), 500.0, "Main Account", new DateTime(2021, 6, 17, 18, 24, 33, 443, DateTimeKind.Local).AddTicks(7272), "Main Card", new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37") });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CurrentValue", "Description", "DueDate", "Name", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("041cc3ad-2159-4b8e-894e-e700a121b48f"), new DateTime(2021, 6, 2, 18, 24, 33, 443, DateTimeKind.Local).AddTicks(7707), 500.0, "Credit Card Account", new DateTime(2021, 6, 17, 18, 24, 33, 443, DateTimeKind.Local).AddTicks(7711), "Credit", new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38") });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CurrentValue", "Description", "DueDate", "Name", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f"), new DateTime(2021, 6, 2, 18, 24, 33, 443, DateTimeKind.Local).AddTicks(7721), 1000.0, "My Savings", new DateTime(2021, 6, 17, 18, 24, 33, 443, DateTimeKind.Local).AddTicks(7721), "Saving", new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39") });

            migrationBuilder.InsertData(
                table: "Entrances",
                columns: new[] { "Id", "CategoryId", "Description", "Observation", "Ticker", "Type", "Value", "WalletId" },
                values: new object[,]
                {
                    { new Guid("77e77c02-e763-4eff-afa4-d5b37f48436b"), new Guid("18391de4-a10f-404e-8ce7-836f297382bb"), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 2, 772.0, new Guid("040cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("420a0164-097e-4fe0-9d4d-d5bec5a0d6f4"), new Guid("18411de4-a10f-404e-8ce7-836f297382bb"), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, 500.0, new Guid("040cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("88ee76b4-e159-44ff-81a8-922ee1f5172b"), new Guid("18411de4-a10f-404e-8ce7-836f297382bb"), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 2, 718.0, new Guid("041cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("484165aa-cb5a-41a6-9a2c-351b54f406ed"), new Guid("18431de4-a10f-404e-8ce7-836f297382bb"), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, 499.0, new Guid("041cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("8cbc6a55-3f53-46e2-9159-31646126d285"), new Guid("18381de4-a10f-404e-8ce7-836f297382bb"), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, 672.0, new Guid("041cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("f401cadb-b05d-4c36-aee0-151f0f6469ac"), new Guid("18411de4-a10f-404e-8ce7-836f297382bb"), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 2, 377.0, new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("f3e72eb0-47e4-4cce-ace0-40455baf9d78"), new Guid("18461de4-a10f-404e-8ce7-836f297382bb"), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 2, 976.0, new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("83d080a9-07b2-4db6-9a96-443953c97b55"), new Guid("18421de4-a10f-404e-8ce7-836f297382bb"), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 2, 771.0, new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("9b862cec-5971-4ffc-b82e-934fbb0af755"), new Guid("18411de4-a10f-404e-8ce7-836f297382bb"), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 2, 524.0, new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("a60f23ae-0c03-4d9c-8e31-8a4d8a73c478"), new Guid("18371de4-a10f-404e-8ce7-836f297382bb"), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 2, 162.0, new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrances_CategoryId",
                table: "Entrances",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrances_Id_CategoryId",
                table: "Entrances",
                columns: new[] { "Id", "CategoryId" })
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Entrances_WalletId",
                table: "Entrances",
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
                name: "Entrances");

            migrationBuilder.DropTable(
                name: "Logs");

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
