using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class create_tables : Migration
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
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 5, 25, 7, 40, 32, 651, DateTimeKind.Local).AddTicks(9684)),
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("18301de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8360), "Salary", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8366), null },
                    { new Guid("18461de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8472), "Other Expanses", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8473), null },
                    { new Guid("18451de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8468), "Home", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8469), null },
                    { new Guid("18441de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8461), "Gifts", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8462), null },
                    { new Guid("18421de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8454), "Travel", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8455), null },
                    { new Guid("18411de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8448), "Education", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8448), null },
                    { new Guid("18401de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8444), "Health", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8445), null },
                    { new Guid("18431de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8458), "Work", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8459), null },
                    { new Guid("18381de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8438), "Transport", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8439), null },
                    { new Guid("18371de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8432), "Food", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8433), null },
                    { new Guid("18361de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8429), "Investiments", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8430), null },
                    { new Guid("18351de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8394), "Other Earnings", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8405), null },
                    { new Guid("18341de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8390), "Loans", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8391), null },
                    { new Guid("18391de4-a10f-404e-8ce7-836f297382bb"), null, new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8441), "Services", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(8442), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new DateTime(2021, 5, 25, 7, 40, 32, 800, DateTimeKind.Local).AddTicks(8161), "admin@admin.com", "Admin", "$2a$11$Vb4cA74Itap3Q0Gw5hE0Fu906gzT..1fzxLDM5DfhnUs6TBx3Tks.", new DateTime(2021, 5, 25, 7, 40, 32, 800, DateTimeKind.Local).AddTicks(8701) },
                    { new Guid("cb43d078-87f1-4864-853a-e626922b8109"), new DateTime(2021, 5, 25, 7, 40, 32, 937, DateTimeKind.Local).AddTicks(9139), "testUser01@email.com", "Test-User-01", "$2a$11$wjZSiUSOHjWWSXb.KQWf7.znSyuF9pHRWZU4GuhsHtS5lrQgn9XTC", new DateTime(2021, 5, 25, 7, 40, 32, 937, DateTimeKind.Local).AddTicks(9167) }
                });

            migrationBuilder.InsertData(
                table: "WalletTypes",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e40"), new DateTime(2021, 5, 25, 7, 40, 32, 939, DateTimeKind.Local).AddTicks(6277), "Investiments", new DateTime(2021, 5, 25, 7, 40, 32, 939, DateTimeKind.Local).AddTicks(6280) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37"), new DateTime(2021, 5, 25, 7, 40, 32, 939, DateTimeKind.Local).AddTicks(5058), "Checking Account", new DateTime(2021, 5, 25, 7, 40, 32, 939, DateTimeKind.Local).AddTicks(5070) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38"), new DateTime(2021, 5, 25, 7, 40, 32, 939, DateTimeKind.Local).AddTicks(5512), "Credit", new DateTime(2021, 5, 25, 7, 40, 32, 939, DateTimeKind.Local).AddTicks(5516) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39"), new DateTime(2021, 5, 25, 7, 40, 32, 939, DateTimeKind.Local).AddTicks(5903), "Saving", new DateTime(2021, 5, 25, 7, 40, 32, 939, DateTimeKind.Local).AddTicks(5907) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e41"), new DateTime(2021, 5, 25, 7, 40, 32, 939, DateTimeKind.Local).AddTicks(6641), "Stocks", new DateTime(2021, 5, 25, 7, 40, 32, 939, DateTimeKind.Local).AddTicks(6645) }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("040cc3ad-2159-4b8e-894e-e700a121b48f"), new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(2774), new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3584), 500.0, "Main Account", new DateTime(2021, 6, 9, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3131), "Main Card", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3588), new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37") });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("041cc3ad-2159-4b8e-894e-e700a121b48f"), new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3617), new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3624), 500.0, "Credit Card Account", new DateTime(2021, 6, 9, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3618), "Credit", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3625), new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38") });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f"), new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3630), new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3632), 1000.0, "My Savings", new DateTime(2021, 6, 9, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3631), "Saving", new DateTime(2021, 5, 25, 7, 40, 32, 940, DateTimeKind.Local).AddTicks(3633), new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39") });

            migrationBuilder.InsertData(
                table: "Entrances",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Observation", "Ticker", "Type", "UpdatedAt", "Value", "WalletId" },
                values: new object[,]
                {
                    { new Guid("5f30bc11-8428-490e-98ea-221a54c5fdb3"), new Guid("18371de4-a10f-404e-8ce7-836f297382bb"), new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5444), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 2, new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5445), 545.0, new Guid("040cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("9e82792b-4c5b-4a5a-98d7-121a06111a53"), new Guid("18371de4-a10f-404e-8ce7-836f297382bb"), new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5463), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5463), 152.0, new Guid("040cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("a41e79dc-c28a-4a23-984d-f95a873583a6"), new Guid("18401de4-a10f-404e-8ce7-836f297382bb"), new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5485), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5486), 197.0, new Guid("040cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("8b117284-3b94-4a89-a5b0-320ba8d5bcbe"), new Guid("18451de4-a10f-404e-8ce7-836f297382bb"), new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5407), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5408), 796.0, new Guid("041cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("03bddc72-b67f-47ef-9767-51fc9ab24511"), new Guid("18371de4-a10f-404e-8ce7-836f297382bb"), new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5449), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5450), 733.0, new Guid("041cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("080d03ab-ce30-41ad-a243-598f800bdba7"), new Guid("18371de4-a10f-404e-8ce7-836f297382bb"), new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5478), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5479), 608.0, new Guid("041cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("d5e73088-6288-48ce-880f-d67cdc6a0ba8"), new Guid("18451de4-a10f-404e-8ce7-836f297382bb"), new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5364), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5370), 810.0, new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("1014f49a-7507-4276-ae98-16083fd1ccdc"), new Guid("18381de4-a10f-404e-8ce7-836f297382bb"), new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5438), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5439), 755.0, new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("7350c925-3a69-4f15-98e4-eb6a1606c948"), new Guid("18461de4-a10f-404e-8ce7-836f297382bb"), new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5457), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5458), 823.0, new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f") },
                    { new Guid("ffc2decc-7a3a-4a5d-bcf0-47683e49250e"), new Guid("18461de4-a10f-404e-8ce7-836f297382bb"), new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5473), "Lorem Ipsum", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", null, 1, new DateTime(2021, 5, 25, 7, 40, 32, 941, DateTimeKind.Local).AddTicks(5474), 590.0, new Guid("042cc3ad-2159-4b8e-894e-e700a121b48f") }
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
