using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations.BookAutorDb
{
    public partial class ChangeNameOfBookVarAddNewVar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkToYaBook",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkToYaBook",
                table: "Books");
        }
    }
}
