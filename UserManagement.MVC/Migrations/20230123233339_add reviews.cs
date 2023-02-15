using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.MVC.Migrations
{
    public partial class addreviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Reviews",
               columns: table => new
               {
                   Id = table.Column<string>(nullable: false),
                   UserId = table.Column<string>(nullable: false),
                   busID = table.Column<string>(nullable: false),
                   Comment = table.Column<string>(maxLength: 256, nullable: true),
                   ConcurrencyStamp = table.Column<string>(nullable: true)
               }
              );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
