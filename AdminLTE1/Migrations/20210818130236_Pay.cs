using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminLTE1.Migrations
{
    public partial class Pay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderAPI",
                newName: "Invoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Invoice",
                table: "OrderAPI",
                newName: "OrderId");
        }
    }
}
