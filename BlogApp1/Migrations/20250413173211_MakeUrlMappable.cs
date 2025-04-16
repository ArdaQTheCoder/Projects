using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp1.Migrations
{
    /// <inheritdoc />
    public partial class MakeUrlMappable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Tags");
        }
    }
}
