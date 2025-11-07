using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationApi1.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category_id",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Supplier_id",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_Category_id",
                table: "products",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_Supplier_id",
                table: "products",
                column: "Supplier_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Categories_Category_id",
                table: "products",
                column: "Category_id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_products_Suppliers_Supplier_id",
                table: "products",
                column: "Supplier_id",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Categories_Category_id",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_Suppliers_Supplier_id",
                table: "products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_products_Category_id",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_Supplier_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Category_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Supplier_id",
                table: "products");
        }
    }
}
