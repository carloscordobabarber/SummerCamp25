using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyData.Migrations
{
    /// <inheritdoc />
    public partial class Statusupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "Users",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ContractPdf",
                table: "Rentals",
                type: "varbinary(max)",
                maxLength: 10485760,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "Buildings",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "Apartments",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContractPdf",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Apartments");
        }
    }
}
