using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NPista.Data.Migrations
{
    public partial class CorrecoesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bandeira",
                table: "Cartoes",
                newName: "Bandeira");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCompra",
                table: "Compras",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 5, 17, 28, 2, 870, DateTimeKind.Local).AddTicks(2572),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 1, 5, 14, 49, 8, 948, DateTimeKind.Local).AddTicks(4246));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bandeira",
                table: "Cartoes",
                newName: "bandeira");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCompra",
                table: "Compras",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 5, 14, 49, 8, 948, DateTimeKind.Local).AddTicks(4246),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 1, 5, 17, 28, 2, 870, DateTimeKind.Local).AddTicks(2572));
        }
    }
}
