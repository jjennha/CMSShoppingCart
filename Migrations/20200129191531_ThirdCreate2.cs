using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsShoppingCart.Migrations
{
    public partial class ThirdCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image",
                table: "Products",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "image");
        }
    }
}
