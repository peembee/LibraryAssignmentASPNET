using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAssignment.Migrations
{
    /// <inheritdoc />
    public partial class Capital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customerBookLists_Books_FK_BookID",
                table: "customerBookLists");

            migrationBuilder.DropForeignKey(
                name: "FK_customerBookLists_Customers_FK_CustomerID",
                table: "customerBookLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customerBookLists",
                table: "customerBookLists");

            migrationBuilder.RenameTable(
                name: "customerBookLists",
                newName: "CustomerBookLists");

            migrationBuilder.RenameIndex(
                name: "IX_customerBookLists_FK_CustomerID",
                table: "CustomerBookLists",
                newName: "IX_CustomerBookLists_FK_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_customerBookLists_FK_BookID",
                table: "CustomerBookLists",
                newName: "IX_CustomerBookLists_FK_BookID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerBookLists",
                table: "CustomerBookLists",
                column: "CustomerBookListID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBookLists_Books_FK_BookID",
                table: "CustomerBookLists",
                column: "FK_BookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBookLists_Customers_FK_CustomerID",
                table: "CustomerBookLists",
                column: "FK_CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBookLists_Books_FK_BookID",
                table: "CustomerBookLists");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBookLists_Customers_FK_CustomerID",
                table: "CustomerBookLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerBookLists",
                table: "CustomerBookLists");

            migrationBuilder.RenameTable(
                name: "CustomerBookLists",
                newName: "customerBookLists");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBookLists_FK_CustomerID",
                table: "customerBookLists",
                newName: "IX_customerBookLists_FK_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBookLists_FK_BookID",
                table: "customerBookLists",
                newName: "IX_customerBookLists_FK_BookID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customerBookLists",
                table: "customerBookLists",
                column: "CustomerBookListID");

            migrationBuilder.AddForeignKey(
                name: "FK_customerBookLists_Books_FK_BookID",
                table: "customerBookLists",
                column: "FK_BookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_customerBookLists_Customers_FK_CustomerID",
                table: "customerBookLists",
                column: "FK_CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
