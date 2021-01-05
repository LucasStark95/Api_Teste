using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NPista.Data.Migrations
{
    public partial class DeleteCascadeCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produtos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCompra",
                table: "Compras",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 5, 14, 49, 8, 948, DateTimeKind.Local).AddTicks(4246),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 1, 4, 21, 5, 7, 796, DateTimeKind.Local).AddTicks(2060));

            migrationBuilder.AlterColumn<string>(
                name: "Titular",
                table: "Cartoes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Cartoes",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produtos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCompra",
                table: "Compras",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 4, 21, 5, 7, 796, DateTimeKind.Local).AddTicks(2060),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 1, 5, 14, 49, 8, 948, DateTimeKind.Local).AddTicks(4246));

            migrationBuilder.AlterColumn<string>(
                name: "Titular",
                table: "Cartoes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Cartoes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(16)",
                oldMaxLength: 16);
        }
    }
}
