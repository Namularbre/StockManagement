using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagement_Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsEssentialFieldToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEssential",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEssential",
                table: "Products");
        }
    }
}
