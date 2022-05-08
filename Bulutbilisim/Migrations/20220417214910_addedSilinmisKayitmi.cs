using Microsoft.EntityFrameworkCore.Migrations;

namespace Bulutbilisim.Migrations
{
    public partial class addedSilinmisKayitmi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SilinmisKayitMi",
                table: "Denemes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SilinmisKayitMi",
                table: "Denemes");
        }
    }
}
