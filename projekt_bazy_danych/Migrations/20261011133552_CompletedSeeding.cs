using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace projekt_bazy_danych.Migrations
{
    /// <inheritdoc />
    public partial class CompletedSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pojazdy",
                columns: new[] { "Vin_pojazdu", "Fabryka", "Moc", "Model", "Pojemnosc", "Typ_silnika" },
                values: new object[,]
                {
                    { 100001, 0, 70, "Fiat 500", 1242, 0 },
                    { 100002, 1, 156, "Jeep Avenger", 0, 4 },
                    { 100003, 2, 160, "Alfa Romeo Tonale", 1469, 2 },
                    { 100004, 0, 100, "Lancia Ypsilon", 1199, 2 },
                    { 100005, 1, 69, "Fiat Panda", 1242, 0 },
                    { 100006, 2, 70, "Fiat 500", 999, 2 },
                    { 100007, 0, 100, "Jeep Avenger", 1199, 0 },
                    { 100008, 1, 130, "Alfa Romeo Tonale", 1598, 1 },
                    { 100009, 2, 156, "Lancia Ypsilon", 0, 4 },
                    { 100010, 0, 70, "Fiat Panda", 999, 2 },
                    { 100011, 1, 118, "Fiat 500", 0, 4 },
                    { 100012, 2, 136, "Jeep Avenger", 1199, 2 },
                    { 100013, 0, 130, "Alfa Romeo Tonale", 1469, 2 },
                    { 100014, 1, 100, "Lancia Ypsilon", 1199, 2 },
                    { 100015, 2, 69, "Fiat Panda", 1242, 0 },
                    { 100016, 0, 70, "Fiat 500", 1242, 0 },
                    { 100017, 1, 156, "Jeep Avenger", 0, 4 },
                    { 100018, 2, 130, "Alfa Romeo Tonale", 1598, 1 },
                    { 100019, 0, 100, "Lancia Ypsilon", 1199, 0 },
                    { 100020, 1, 70, "Fiat Panda", 999, 2 },
                    { 100021, 2, 70, "Fiat 500", 999, 2 },
                    { 100022, 0, 100, "Jeep Avenger", 1199, 0 },
                    { 100023, 1, 160, "Alfa Romeo Tonale", 1469, 2 },
                    { 100024, 2, 156, "Lancia Ypsilon", 0, 4 },
                    { 100025, 0, 69, "Fiat Panda", 1242, 0 },
                    { 100026, 1, 118, "Fiat 500", 0, 4 },
                    { 100027, 2, 136, "Jeep Avenger", 1199, 2 },
                    { 100028, 0, 130, "Alfa Romeo Tonale", 1469, 2 },
                    { 100029, 1, 100, "Lancia Ypsilon", 1199, 2 },
                    { 100030, 2, 70, "Fiat Panda", 999, 2 },
                    { 100031, 0, 70, "Fiat 500", 1242, 0 },
                    { 100032, 1, 156, "Jeep Avenger", 0, 4 },
                    { 100033, 2, 130, "Alfa Romeo Tonale", 1598, 1 },
                    { 100034, 0, 100, "Lancia Ypsilon", 1199, 0 },
                    { 100035, 1, 69, "Fiat Panda", 1242, 0 },
                    { 100036, 2, 70, "Fiat 500", 999, 2 },
                    { 100037, 0, 100, "Jeep Avenger", 1199, 0 },
                    { 100038, 1, 160, "Alfa Romeo Tonale", 1469, 2 },
                    { 100039, 2, 156, "Lancia Ypsilon", 0, 4 },
                    { 100040, 0, 70, "Fiat Panda", 999, 2 },
                    { 100041, 1, 118, "Fiat 500", 0, 4 },
                    { 100042, 2, 136, "Jeep Avenger", 1199, 2 },
                    { 100043, 0, 130, "Alfa Romeo Tonale", 1469, 2 },
                    { 100044, 1, 100, "Lancia Ypsilon", 1199, 2 },
                    { 100045, 2, 69, "Fiat Panda", 1242, 0 },
                    { 100046, 0, 70, "Fiat 500", 1242, 0 },
                    { 100047, 1, 156, "Jeep Avenger", 0, 4 },
                    { 100048, 2, 130, "Alfa Romeo Tonale", 1598, 1 },
                    { 100049, 0, 100, "Lancia Ypsilon", 1199, 0 },
                    { 100050, 1, 70, "Fiat Panda", 999, 2 }
                });

            migrationBuilder.InsertData(
                table: "Zamowienia",
                columns: new[] { "Id_zamowienia", "Data", "KlientId", "Koszt", "Vin_pojazdu" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(2023, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000001"), 65000, 100001 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(2023, 2, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000002"), 140000, 100002 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new DateTime(2023, 3, 22, 9, 15, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000003"), 155000, 100003 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new DateTime(2023, 4, 5, 14, 45, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000004"), 95000, 100004 },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new DateTime(2023, 5, 18, 16, 20, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000005"), 55000, 100005 },
                    { new Guid("20000000-0000-0000-0000-000000000006"), new DateTime(2023, 6, 30, 10, 10, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000006"), 72000, 100006 },
                    { new Guid("20000000-0000-0000-0000-000000000007"), new DateTime(2023, 7, 21, 8, 5, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000007"), 110000, 100007 },
                    { new Guid("20000000-0000-0000-0000-000000000008"), new DateTime(2023, 8, 11, 13, 40, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000008"), 145000, 100008 },
                    { new Guid("20000000-0000-0000-0000-000000000009"), new DateTime(2023, 9, 2, 15, 55, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000009"), 160000, 100009 },
                    { new Guid("20000000-0000-0000-0000-000000000010"), new DateTime(2023, 10, 14, 9, 30, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000010"), 68000, 100010 },
                    { new Guid("20000000-0000-0000-0000-000000000011"), new DateTime(2023, 11, 25, 11, 45, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000011"), 115000, 100011 },
                    { new Guid("20000000-0000-0000-0000-000000000012"), new DateTime(2023, 12, 5, 14, 20, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000012"), 135000, 100012 },
                    { new Guid("20000000-0000-0000-0000-000000000013"), new DateTime(2024, 1, 18, 10, 15, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000013"), 142000, 100013 },
                    { new Guid("20000000-0000-0000-0000-000000000014"), new DateTime(2024, 2, 28, 16, 50, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000014"), 98000, 100014 },
                    { new Guid("20000000-0000-0000-0000-000000000015"), new DateTime(2024, 3, 10, 9, 5, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000015"), 56000, 100015 },
                    { new Guid("20000000-0000-0000-0000-000000000016"), new DateTime(2024, 4, 12, 12, 35, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000016"), 64000, 100016 },
                    { new Guid("20000000-0000-0000-0000-000000000017"), new DateTime(2024, 5, 24, 15, 10, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000017"), 142000, 100017 },
                    { new Guid("20000000-0000-0000-0000-000000000018"), new DateTime(2024, 6, 7, 8, 50, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000018"), 147000, 100018 },
                    { new Guid("20000000-0000-0000-0000-000000000019"), new DateTime(2024, 7, 19, 11, 20, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000019"), 97000, 100019 },
                    { new Guid("20000000-0000-0000-0000-000000000020"), new DateTime(2024, 8, 31, 14, 40, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000020"), 71000, 100020 },
                    { new Guid("20000000-0000-0000-0000-000000000021"), new DateTime(2024, 9, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000021"), 70000, 100021 },
                    { new Guid("20000000-0000-0000-0000-000000000022"), new DateTime(2024, 10, 22, 13, 30, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000022"), 108000, 100022 },
                    { new Guid("20000000-0000-0000-0000-000000000023"), new DateTime(2024, 11, 4, 16, 15, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000023"), 158000, 100023 },
                    { new Guid("20000000-0000-0000-0000-000000000024"), new DateTime(2024, 12, 11, 9, 45, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000024"), 141000, 100024 },
                    { new Guid("20000000-0000-0000-0000-000000000025"), new DateTime(2025, 1, 23, 11, 10, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000025"), 57000, 100025 },
                    { new Guid("20000000-0000-0000-0000-000000000026"), new DateTime(2025, 2, 14, 14, 25, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000026"), 116000, 100026 },
                    { new Guid("20000000-0000-0000-0000-000000000027"), new DateTime(2025, 3, 5, 8, 35, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000027"), 133000, 100027 },
                    { new Guid("20000000-0000-0000-0000-000000000028"), new DateTime(2025, 4, 18, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000028"), 143000, 100028 },
                    { new Guid("20000000-0000-0000-0000-000000000029"), new DateTime(2025, 5, 29, 15, 40, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000029"), 99000, 100029 },
                    { new Guid("20000000-0000-0000-0000-000000000030"), new DateTime(2025, 6, 10, 10, 20, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000030"), 69000, 100030 },
                    { new Guid("20000000-0000-0000-0000-000000000031"), new DateTime(2025, 7, 21, 13, 10, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000031"), 66000, 100031 },
                    { new Guid("20000000-0000-0000-0000-000000000032"), new DateTime(2025, 8, 5, 9, 50, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000032"), 139000, 100032 },
                    { new Guid("20000000-0000-0000-0000-000000000033"), new DateTime(2025, 9, 17, 14, 35, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000033"), 146000, 100033 },
                    { new Guid("20000000-0000-0000-0000-000000000034"), new DateTime(2025, 10, 28, 11, 5, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000034"), 102000, 100034 },
                    { new Guid("20000000-0000-0000-0000-000000000035"), new DateTime(2025, 11, 9, 16, 20, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000035"), 58000, 100035 },
                    { new Guid("20000000-0000-0000-0000-000000000036"), new DateTime(2025, 12, 14, 8, 45, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000036"), 73000, 100036 },
                    { new Guid("20000000-0000-0000-0000-000000000037"), new DateTime(2026, 1, 25, 12, 15, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000037"), 109000, 100037 },
                    { new Guid("20000000-0000-0000-0000-000000000038"), new DateTime(2026, 2, 18, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000038"), 159000, 100038 },
                    { new Guid("20000000-0000-0000-0000-000000000039"), new DateTime(2026, 3, 30, 10, 30, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000039"), 144000, 100039 },
                    { new Guid("20000000-0000-0000-0000-000000000040"), new DateTime(2026, 4, 12, 14, 55, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000040"), 72000, 100040 },
                    { new Guid("20000000-0000-0000-0000-000000000041"), new DateTime(2026, 5, 23, 9, 10, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000041"), 117000, 100041 },
                    { new Guid("20000000-0000-0000-0000-000000000042"), new DateTime(2026, 6, 4, 12, 40, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000042"), 134000, 100042 },
                    { new Guid("20000000-0000-0000-0000-000000000043"), new DateTime(2026, 7, 16, 15, 25, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000043"), 148000, 100043 },
                    { new Guid("20000000-0000-0000-0000-000000000044"), new DateTime(2026, 8, 28, 8, 50, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000044"), 101000, 100044 },
                    { new Guid("20000000-0000-0000-0000-000000000045"), new DateTime(2026, 9, 9, 13, 15, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000045"), 59000, 100045 },
                    { new Guid("20000000-0000-0000-0000-000000000046"), new DateTime(2026, 10, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000046"), 67000, 100046 },
                    { new Guid("20000000-0000-0000-0000-000000000047"), new DateTime(2026, 11, 13, 10, 45, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000047"), 140000, 100047 },
                    { new Guid("20000000-0000-0000-0000-000000000048"), new DateTime(2026, 12, 25, 14, 20, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000048"), 149000, 100048 },
                    { new Guid("20000000-0000-0000-0000-000000000049"), new DateTime(2027, 1, 7, 9, 35, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000049"), 103000, 100049 },
                    { new Guid("20000000-0000-0000-0000-000000000050"), new DateTime(2027, 2, 18, 12, 10, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000050"), 75000, 100050 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Zamowienia",
                keyColumn: "Id_zamowienia",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100001);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100002);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100003);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100004);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100005);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100006);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100007);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100008);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100009);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100010);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100011);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100012);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100013);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100014);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100015);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100016);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100017);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100018);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100019);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100020);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100021);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100022);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100023);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100024);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100025);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100026);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100027);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100028);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100029);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100030);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100031);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100032);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100033);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100034);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100035);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100036);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100037);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100038);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100039);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100040);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100041);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100042);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100043);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100044);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100045);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100046);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100047);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100048);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100049);

            migrationBuilder.DeleteData(
                table: "Pojazdy",
                keyColumn: "Vin_pojazdu",
                keyValue: 100050);
        }
    }
}
