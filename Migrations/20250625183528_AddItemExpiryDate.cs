using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvantoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddItemExpiryDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Items",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Items");
        }
    }
}
