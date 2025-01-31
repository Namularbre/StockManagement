using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagement_Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRedondantField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdStorage",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdStorage",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
