using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartProfil.Migrations
{
    public partial class AddOrderFormInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderFormInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    NameOnCard = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpMonth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderFormInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderFormInfo");
        }
    }
}
