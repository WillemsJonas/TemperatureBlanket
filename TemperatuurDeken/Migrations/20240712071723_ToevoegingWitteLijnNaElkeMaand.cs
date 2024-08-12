using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TemperatuurDeken.Migrations
{
    /// <inheritdoc />
    public partial class ToevoegingWitteLijnNaElkeMaand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dagen_Datum",
                table: "Dagen");

            migrationBuilder.InsertData(
                table: "Dagen",
                columns: new[] { "DagId", "Datum", "Gemaakt", "Kleur", "Temperatuur" },
                values: new object[,]
                {
                    { 367, new DateOnly(2024, 1, 31), false, 3, null },
                    { 368, new DateOnly(2024, 2, 29), false, 3, null },
                    { 369, new DateOnly(2024, 3, 31), false, 3, null },
                    { 370, new DateOnly(2024, 4, 30), false, 3, null },
                    { 371, new DateOnly(2024, 5, 31), false, 3, null },
                    { 372, new DateOnly(2024, 6, 30), false, 3, null },
                    { 373, new DateOnly(2024, 7, 31), false, 3, null },
                    { 374, new DateOnly(2024, 8, 31), false, 3, null },
                    { 375, new DateOnly(2024, 9, 30), false, 3, null },
                    { 376, new DateOnly(2024, 10, 31), false, 3, null },
                    { 377, new DateOnly(2024, 11, 30), false, 3, null },
                    { 378, new DateOnly(2024, 12, 31), false, 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dagen_Datum",
                table: "Dagen",
                column: "Datum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dagen_Datum",
                table: "Dagen");

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "Dagen",
                keyColumn: "DagId",
                keyValue: 378);

            migrationBuilder.CreateIndex(
                name: "IX_Dagen_Datum",
                table: "Dagen",
                column: "Datum",
                unique: true);
        }
    }
}
