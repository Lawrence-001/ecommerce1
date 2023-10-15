using Microsoft.EntityFrameworkCore.Migrations;

namespace e_commerce.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategory = table.Column<int>(type: "int", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Cost", "Description", "ImgUrl", "Name", "ProductCategory" },
                values: new object[,]
                {
                    { 1, 500.0, "plastic chair", null, "Plastic chair", 1 },
                    { 2, 5000.0, "Office chair", null, "Office chair", 1 },
                    { 3, 50000.0, "Laptop", null, "Hp Laptop", 0 },
                    { 4, 5000.0, "Utensils", null, "Pressure cooker", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
