using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NPista.Data.Migrations
{
    public partial class IncludeCampoDataCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCompra",
                table: "Compras",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 4, 21, 5, 7, 796, DateTimeKind.Local).AddTicks(2060));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCompra",
                table: "Compras");
        }
    }
}
