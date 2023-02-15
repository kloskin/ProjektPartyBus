using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.MVC.Migrations
{
    public partial class AddBus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Bus",
               columns: table => new
               {
                   busID = table.Column<int>(nullable: false),
                   model = table.Column<string>(nullable: false),
                   hourPrice = table.Column<float>(nullable: false),
                   maxPeople = table.Column<int>(nullable:true)
                   
               }
               );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
