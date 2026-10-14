using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt_bazy_danych.Migrations
{
    /// <inheritdoc />
    public partial class dodaniePeselu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pesel",
                table: "Klienci",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "Pesel",
                value: "81021439281");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "Pesel",
                value: "92110547284");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "Pesel",
                value: "75082194827");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "Pesel",
                value: "63041928475");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "Pesel",
                value: "88091238471");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "Pesel",
                value: "95013148293");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "Pesel",
                value: "54120839284");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                column: "Pesel",
                value: "77062493821");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                column: "Pesel",
                value: "83051728374");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                column: "Pesel",
                value: "91080347285");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "Pesel",
                value: "89112239482");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "Pesel",
                value: "48031528374");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "Pesel",
                value: "86072948572");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"),
                column: "Pesel",
                value: "61101839281");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000015"),
                column: "Pesel",
                value: "94021448293");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000016"),
                column: "Pesel",
                value: "72092538475");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000017"),
                column: "Pesel",
                value: "98120647281");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000018"),
                column: "Pesel",
                value: "55041129384");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000019"),
                column: "Pesel",
                value: "87061938472");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000020"),
                column: "Pesel",
                value: "42113049283");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000021"),
                column: "Pesel",
                value: "99032148291");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000022"),
                column: "Pesel",
                value: "59081439285");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000023"),
                column: "Pesel",
                value: "96102847283");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000024"),
                column: "Pesel",
                value: "84011538471");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000025"),
                column: "Pesel",
                value: "93050949284");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000026"),
                column: "Pesel",
                value: "79122238475");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000027"),
                column: "Pesel",
                value: "97071848292");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000028"),
                column: "Pesel",
                value: "51020539281");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000029"),
                column: "Pesel",
                value: "01291447283");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000030"),
                column: "Pesel",
                value: "58062738472");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000031"),
                column: "Pesel",
                value: "85041249285");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000032"),
                column: "Pesel",
                value: "49110839284");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000033"),
                column: "Pesel",
                value: "90082548291");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000034"),
                column: "Pesel",
                value: "53011938475");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000035"),
                column: "Pesel",
                value: "92100447282");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000036"),
                column: "Pesel",
                value: "88032839281");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000037"),
                column: "Pesel",
                value: "95071148294");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000038"),
                column: "Pesel",
                value: "45092638473");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000039"),
                column: "Pesel",
                value: "73051449285");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000040"),
                column: "Pesel",
                value: "80120239282");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000041"),
                column: "Pesel",
                value: "68041748291");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000042"),
                column: "Pesel",
                value: "91022338474");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000043"),
                column: "Pesel",
                value: "76110947285");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000044"),
                column: "Pesel",
                value: "82063039281");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000045"),
                column: "Pesel",
                value: "70091548293");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000046"),
                column: "Pesel",
                value: "86010738472");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000047"),
                column: "Pesel",
                value: "94082149284");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000048"),
                column: "Pesel",
                value: "56031239285");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000049"),
                column: "Pesel",
                value: "97120548291");

            migrationBuilder.UpdateData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000050"),
                column: "Pesel",
                value: "89052938473");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pesel",
                table: "Klienci");
        }
    }
}
