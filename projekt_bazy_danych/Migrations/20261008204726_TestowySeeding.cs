using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace projekt_bazy_danych.Migrations
{
    /// <inheritdoc />
    public partial class TestowySeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Klienci",
                columns: new[] { "Id", "Imie", "Nazwisko" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "Jan", "Kowalski" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Anna", "Nowak" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "Piotr", "Wisniewski" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "Maria", "Wojcik" },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "Krzysztof", "Kowalczyk" },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "Katarzyna", "Kaminska" },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "Andrzej", "Lewandowski" },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "Malgorzata", "Zielinska" },
                    { new Guid("10000000-0000-0000-0000-000000000009"), "Tomasz", "Szymanski" },
                    { new Guid("10000000-0000-0000-0000-000000000010"), "Agnieszka", "Wozniak" },
                    { new Guid("10000000-0000-0000-0000-000000000011"), "Michal", "Dabrowski" },
                    { new Guid("10000000-0000-0000-0000-000000000012"), "Krystyna", "Kozlowska" },
                    { new Guid("10000000-0000-0000-0000-000000000013"), "Pawel", "Jankowski" },
                    { new Guid("10000000-0000-0000-0000-000000000014"), "Barbara", "Mazur" },
                    { new Guid("10000000-0000-0000-0000-000000000015"), "Marcin", "Kwiatkowski" },
                    { new Guid("10000000-0000-0000-0000-000000000016"), "Ewa", "Krawczyk" },
                    { new Guid("10000000-0000-0000-0000-000000000017"), "Kamil", "Kaczmarek" },
                    { new Guid("10000000-0000-0000-0000-000000000018"), "Elzbieta", "Piotrowska" },
                    { new Guid("10000000-0000-0000-0000-000000000019"), "Lukasz", "Grabowski" },
                    { new Guid("10000000-0000-0000-0000-000000000020"), "Zofia", "Zajac" },
                    { new Guid("10000000-0000-0000-0000-000000000021"), "Jakub", "Pawlowski" },
                    { new Guid("10000000-0000-0000-0000-000000000022"), "Teresa", "Michalska" },
                    { new Guid("10000000-0000-0000-0000-000000000023"), "Mateusz", "Krol" },
                    { new Guid("10000000-0000-0000-0000-000000000024"), "Magdalena", "Wieczorek" },
                    { new Guid("10000000-0000-0000-0000-000000000025"), "Szymon", "Jablonski" },
                    { new Guid("10000000-0000-0000-0000-000000000026"), "Joanna", "Wrobel" },
                    { new Guid("10000000-0000-0000-0000-000000000027"), "Dawid", "Nowakowski" },
                    { new Guid("10000000-0000-0000-0000-000000000028"), "Jadwiga", "Majewska" },
                    { new Guid("10000000-0000-0000-0000-000000000029"), "Kacper", "Olszewski" },
                    { new Guid("10000000-0000-0000-0000-000000000030"), "Danuta", "Stepien" },
                    { new Guid("10000000-0000-0000-0000-000000000031"), "Maciej", "Malinowski" },
                    { new Guid("10000000-0000-0000-0000-000000000032"), "Halina", "Jaworska" },
                    { new Guid("10000000-0000-0000-0000-000000000033"), "Aleksander", "Adamczyk" },
                    { new Guid("10000000-0000-0000-0000-000000000034"), "Irena", "Dudek" },
                    { new Guid("10000000-0000-0000-0000-000000000035"), "Bartlomiej", "Gorski" },
                    { new Guid("10000000-0000-0000-0000-000000000036"), "Ewelina", "Pawlak" },
                    { new Guid("10000000-0000-0000-0000-000000000037"), "Sebastian", "Sikora" },
                    { new Guid("10000000-0000-0000-0000-000000000038"), "Janina", "Walczak" },
                    { new Guid("10000000-0000-0000-0000-000000000039"), "Robert", "Rutkowski" },
                    { new Guid("10000000-0000-0000-0000-000000000040"), "Beata", "Michalak" },
                    { new Guid("10000000-0000-0000-0000-000000000041"), "Dariusz", "Szewczyk" },
                    { new Guid("10000000-0000-0000-0000-000000000042"), "Aleksandra", "Ostrowska" },
                    { new Guid("10000000-0000-0000-0000-000000000043"), "Artur", "Tomaszewski" },
                    { new Guid("10000000-0000-0000-0000-000000000044"), "Marta", "Pietrzak" },
                    { new Guid("10000000-0000-0000-0000-000000000045"), "Mariusz", "Zalewski" },
                    { new Guid("10000000-0000-0000-0000-000000000046"), "Dorota", "Wlodarczyk" },
                    { new Guid("10000000-0000-0000-0000-000000000047"), "Rafal", "Maciejewski" },
                    { new Guid("10000000-0000-0000-0000-000000000048"), "Lucyna", "Jastrzebska" },
                    { new Guid("10000000-0000-0000-0000-000000000049"), "Karol", "Dziedzic" },
                    { new Guid("10000000-0000-0000-0000-000000000050"), "Monika", "Sadowska" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Klienci",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000050"));
        }
    }
}
