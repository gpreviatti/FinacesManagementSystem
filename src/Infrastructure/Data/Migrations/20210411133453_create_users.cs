using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class create_users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[] { new Guid("37e4f662-1987-4b9d-b245-ad6014f3217a"), new DateTime(2021, 4, 11, 10, 34, 50, 795, DateTimeKind.Local).AddTicks(2481), "admin@admin.com", "Admin", "$2a$11$x/mNDOsFXIO5ow/0XGkQ8OaSmNtrqBvE/HSHW7KMYw/iai8qiTuda", new DateTime(2021, 4, 11, 10, 34, 50, 797, DateTimeKind.Local).AddTicks(2146) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "UpdatedAt" },
                values: new object[] { new Guid("6823c5ae-ca43-4287-82e1-10d5fca46a2e"), new DateTime(2021, 4, 11, 10, 34, 51, 88, DateTimeKind.Local).AddTicks(3142), "testUser01@email.com", "Test-User-01", "$2a$11$wAphH2ntzKu2SYorng/RPOeFkwWzfyYNpBIX.iOOMHX6E19NNuGom", new DateTime(2021, 4, 11, 10, 34, 51, 88, DateTimeKind.Local).AddTicks(3182) });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
