using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartProfil.Migrations
{
    public partial class AddOrderFormInfoTableWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrderFormInfo",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderFormInfo_UserId",
                table: "OrderFormInfo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderFormInfo_Users_UserId",
                table: "OrderFormInfo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderFormInfo_Users_UserId",
                table: "OrderFormInfo");

            migrationBuilder.DropIndex(
                name: "IX_OrderFormInfo_UserId",
                table: "OrderFormInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderFormInfo");
        }
    }
}
