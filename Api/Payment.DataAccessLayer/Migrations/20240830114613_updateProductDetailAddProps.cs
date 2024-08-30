using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateProductDetailAddProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "ProductDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "ProductDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "ProductDetails");
        }
    }
}
