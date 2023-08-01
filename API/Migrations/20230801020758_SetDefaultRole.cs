using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class SetDefaultRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[] { new Guid("083e7d1a-0ae6-4508-9fa7-08db92312884"), new DateTime(2023, 8, 1, 9, 7, 57, 906, DateTimeKind.Local).AddTicks(2264), new DateTime(2023, 8, 1, 9, 7, 57, 906, DateTimeKind.Local).AddTicks(2276), "Employee" });

            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[] { new Guid("65ac9785-4023-41f0-9fa6-08db92312884"), new DateTime(2023, 8, 1, 9, 7, 57, 906, DateTimeKind.Local).AddTicks(2285), new DateTime(2023, 8, 1, 9, 7, 57, 906, DateTimeKind.Local).AddTicks(2285), "Admin" });

            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[] { new Guid("a165ce23-72d7-4a40-9fa5-08db92312884"), new DateTime(2023, 8, 1, 9, 7, 57, 906, DateTimeKind.Local).AddTicks(2281), new DateTime(2023, 8, 1, 9, 7, 57, 906, DateTimeKind.Local).AddTicks(2281), "Manager" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("083e7d1a-0ae6-4508-9fa7-08db92312884"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("65ac9785-4023-41f0-9fa6-08db92312884"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("a165ce23-72d7-4a40-9fa5-08db92312884"));
        }
    }
}
