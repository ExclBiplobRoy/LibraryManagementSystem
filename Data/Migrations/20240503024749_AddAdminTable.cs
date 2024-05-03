using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddAdminTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminID", "CreatedBy", "DateCreated", "DateModified", "Email", "FirstName", "Level", "ModifiedBy", "Password", "SurName" },
                values: new object[] { 1, null, null, null, "admin@gmail.com", "User", 5, null, "12345", "Admin" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorID", "AuthorBio", "AuthorName", "CreatedBy", "DateCreated", "DateModified", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, "British author best known for writing the Harry Potter series.", "J.K. Rowling", null, null, null, null },
                    { 2, "American author known for his horror, supernatural fiction, and suspense novels.", "Stephen King", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberID", "CreatedBy", "DateCreated", "DateModified", "Email", "FirstName", "LastName", "ModifiedBy", "PhoneNumber", "RegistrationDate" },
                values: new object[,]
                {
                    { 1, null, null, null, "john.doe@example.com", "John", "Doe", null, "1234567890", new DateTime(2024, 5, 3, 8, 47, 49, 573, DateTimeKind.Local).AddTicks(5802) },
                    { 2, null, null, null, "jane.smith@example.com", "Jane", "Smith", null, "9876543210", new DateTime(2024, 5, 3, 8, 47, 49, 573, DateTimeKind.Local).AddTicks(5811) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "AuthorID", "AvailableCopies", "CreatedBy", "DateCreated", "DateModified", "ISBN", "ModifiedBy", "PublishedDate", "Title", "TotalCopies" },
                values: new object[] { 1, 1, 10, null, null, null, "9780747532743", null, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Philosopher's Stone", 10 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "AuthorID", "AvailableCopies", "CreatedBy", "DateCreated", "DateModified", "ISBN", "ModifiedBy", "PublishedDate", "Title", "TotalCopies" },
                values: new object[] { 2, 2, 8, null, null, null, "9780451169518", null, new DateTime(1986, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "It", 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorID",
                keyValue: 2);
        }
    }
}
