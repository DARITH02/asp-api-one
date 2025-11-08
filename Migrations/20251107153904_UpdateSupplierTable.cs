using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationApi1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSupplierTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Suppliers",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Suppliers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Suppliers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Suppliers",
                newName: "Description");
        }
    }
}
