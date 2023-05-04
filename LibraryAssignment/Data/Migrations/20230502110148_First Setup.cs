using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAssignment.Migrations
{
    /// <inheritdoc />
    public partial class FirstSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegisteredBookDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "customerBookLists",
                columns: table => new
                {
                    CustomerBookListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_CustomerID = table.Column<int>(type: "int", nullable: false),
                    FK_BookID = table.Column<int>(type: "int", nullable: false),
                    Retrieved = table.Column<bool>(type: "bit", nullable: true),
                    StartBookedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndBookedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Returned = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerBookLists", x => x.CustomerBookListID);
                    table.ForeignKey(
                        name: "FK_customerBookLists_Books_FK_BookID",
                        column: x => x.FK_BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customerBookLists_Customers_FK_CustomerID",
                        column: x => x.FK_CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customerBookLists_FK_BookID",
                table: "customerBookLists",
                column: "FK_BookID");

            migrationBuilder.CreateIndex(
                name: "IX_customerBookLists_FK_CustomerID",
                table: "customerBookLists",
                column: "FK_CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerBookLists");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
