using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCSampleApp.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientServices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientServices",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientServices", x => new { x.ClientID, x.ServiceID });
                    table.ForeignKey(
                        name: "FK_ClientServices_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientServices_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientServices_ServiceID",
                table: "ClientServices",
                column: "ServiceID");
        }
    }
}
