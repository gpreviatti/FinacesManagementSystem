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
                    { new Guid("0ea381c4-3955-46b2-9642-65602e338f3a"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4404), "Salary", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4409), null },
                    { new Guid("06591ed3-e393-484b-ae85-da4bffb45fb2"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4525), "Utilities", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4526), null },
                    { new Guid("ba1adaf5-6bc0-4561-b3d9-332a4acf22b6"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4521), "Consumer Staples", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4522), null },
                    { new Guid("76ba6c58-3f27-4866-8973-24ee19de784a"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4519), "Health Care", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4520), null },
                    { new Guid("06aa7076-f187-4500-886c-6a5c835a0f14"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4517), "Real Estate", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4517), null },
                    { new Guid("058e6276-dea4-4e9f-b2ce-111cacf8352b"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4515), "Communication Services", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4515), null },
                    { new Guid("fedde108-d1d7-43a7-ac9e-2fcb14524633"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4511), "Information Technology", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4512), null },
                    { new Guid("b95f09f8-55e6-444f-8383-3e33f7536ead"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4509), "Consumer Discretionary", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4510), null },
                    { new Guid("57a4a78b-8349-432b-9c25-caa731a4727c"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4505), "Financials", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4506), null },
                    { new Guid("7016a053-d5fb-47bb-b78a-dccdb7705eae"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4501), "Industrials", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4502), null },
                    { new Guid("d7d072fd-8e6c-460d-83e5-2e2986e02840"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4499), "Gifts", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4500), null },
                    { new Guid("bde6e8b1-5e9b-46d7-872b-5ceb636b78d2"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4507), "Energy", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4508), null },
                    { new Guid("9245f61b-85cd-4ea4-9554-7006afaf84ac"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4460), "Travel", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4461), null },
                    { new Guid("4849827a-3419-4820-8638-43c40bc6a83c"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4427), "Loans", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4428), null },
                    { new Guid("c229e41c-24c4-44bc-80d1-6d573eb3d0c1"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4497), "Work", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4498), null },
                    { new Guid("dd292047-fb69-4970-9908-3671488f62ef"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4432), "Investiments", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4433), null },
                    { new Guid("70b5f1fe-5f29-422b-b75b-0ac5b0ea582e"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4434), "Food", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4435), null },
                    { new Guid("a6b9744a-1923-4eae-ac83-5848da07714c"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4430), "Other Earnings", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4431), null },
                    { new Guid("4f351c55-3892-42ba-958e-f4c763f739cd"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4453), "Services", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4453), null },
                    { new Guid("37442197-62b1-4f6d-a819-afc4fb7724c6"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4455), "Health", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4456), null },
                    { new Guid("7f8a8eaf-c152-4e5d-ac10-8c1677704628"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4457), "Education", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4458), null },
                    { new Guid("9a4f1f28-8534-488e-9767-22d7128729ae"), null, new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4450), "Transport", new DateTime(2021, 5, 1, 11, 55, 43, 723, DateTimeKind.Local).AddTicks(4451), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new DateTime(2021, 5, 1, 11, 55, 43, 591, DateTimeKind.Local).AddTicks(389), "admin@admin.com", "Admin", "$2a$11$Qma.mDclcg9ChOY2y.PzOuf/JZ8G1KHoxSRoukp2wonSG2lYu1i12", new DateTime(2021, 5, 1, 11, 55, 43, 591, DateTimeKind.Local).AddTicks(8711) },
                    { new Guid("cb43d078-87f1-4864-853a-e626922b8109"), new DateTime(2021, 5, 1, 11, 55, 43, 720, DateTimeKind.Local).AddTicks(7439), "testUser01@email.com", "Test-User-01", "$2a$11$0OMcWwg1xp4qcAEi7I566e/6mkoCRcFjAvW.IF0vJI1zGtAJFSrye", new DateTime(2021, 5, 1, 11, 55, 43, 720, DateTimeKind.Local).AddTicks(7467) }
                });

            migrationBuilder.InsertData(
                table: "WalletTypes",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e40"), new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(3697), "Investiments", new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(3699) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37"), new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(2548), "Checking Account", new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(2558) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38"), new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(3082), "Credit", new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(3085) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e39"), new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(3391), "Saving", new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(3394) },
                    { new Guid("ad4ac47f-0888-4d60-81f9-964153b13e41"), new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(3979), "Stocks", new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(3982) }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("b7b68314-e2a6-4200-8299-f6076824d690"), new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(9167), new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(9827), 500.0, "Main Account", new DateTime(2021, 5, 16, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(9451), "Inter", new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(9830), new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e37") });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CloseDate", "CreatedAt", "CurrentValue", "Description", "DueDate", "Name", "UpdatedAt", "UserId", "WalletTypeId" },
                values: new object[] { new Guid("1e6942cd-adce-4f4c-95c7-6e3176a48619"), new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(9857), new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(9863), 500.0, "Credit Card Account", new DateTime(2021, 5, 16, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(9858), "Credit", new DateTime(2021, 5, 1, 11, 55, 43, 722, DateTimeKind.Local).AddTicks(9863), new Guid("430e0144-289f-4a95-8f14-bacfabb3fe8a"), new Guid("ad4ac47f-0888-4d60-81f9-964153b13e38") });

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
