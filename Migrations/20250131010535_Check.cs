using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCSampleApp.Migrations
{
    /// <inheritdoc />
    public partial class Check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ServiceID",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ServiceID",
                table: "Employees",
                column: "ServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Services_ServiceID",
                table: "Employees",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Services_ServiceID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ServiceID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ServiceID",
                table: "Employees");
        }
    }
}
