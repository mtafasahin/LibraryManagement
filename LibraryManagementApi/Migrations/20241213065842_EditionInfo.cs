using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class EditionInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Edition",
                table: "Books",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edition",
                table: "Books");
        }
    }
}
