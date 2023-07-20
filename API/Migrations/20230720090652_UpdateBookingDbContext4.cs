using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class UpdateBookingDbContext4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_employees_tb_m_account_guid",
                table: "tb_m_employees");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_tb_m_employees_guid",
                table: "tb_m_account",
                column: "guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_tb_m_employees_guid",
                table: "tb_m_account");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employees_tb_m_account_guid",
                table: "tb_m_employees",
                column: "guid",
                principalTable: "tb_m_account",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
