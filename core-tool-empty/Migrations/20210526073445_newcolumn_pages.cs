using Microsoft.EntityFrameworkCore.Migrations;

namespace core_tool_empty.Migrations
{
    public partial class newcolumn_pages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Pages",
                table: "tblBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pages",
                table: "tblBooks");
        }
    }
}
