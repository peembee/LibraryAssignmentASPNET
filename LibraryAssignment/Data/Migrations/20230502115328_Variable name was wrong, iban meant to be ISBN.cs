using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAssignment.Migrations
{
    /// <inheritdoc />
    public partial class VariablenamewaswrongibanmeanttobeISBN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Iban",
                table: "Books",
                newName: "ISBN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Books",
                newName: "Iban");
        }
    }
}
