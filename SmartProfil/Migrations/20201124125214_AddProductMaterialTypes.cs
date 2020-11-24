using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartProfil.Migrations
{
    public partial class AddProductMaterialTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductMaterialType_ProductMaterialTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterialType",
                table: "ProductMaterialType");

            migrationBuilder.RenameTable(
                name: "ProductMaterialType",
                newName: "ProductMaterialTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterialTypes",
                table: "ProductMaterialTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductMaterialTypes_ProductMaterialTypeId",
                table: "Products",
                column: "ProductMaterialTypeId",
                principalTable: "ProductMaterialTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductMaterialTypes_ProductMaterialTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterialTypes",
                table: "ProductMaterialTypes");

            migrationBuilder.RenameTable(
                name: "ProductMaterialTypes",
                newName: "ProductMaterialType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterialType",
                table: "ProductMaterialType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductMaterialType_ProductMaterialTypeId",
                table: "Products",
                column: "ProductMaterialTypeId",
                principalTable: "ProductMaterialType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
