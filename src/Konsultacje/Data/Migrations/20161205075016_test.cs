using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Konsultacje.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Konsultacja",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Budynek = table.Column<int>(nullable: false),
                    PracownikUczelniId = table.Column<string>(nullable: true),
                    Sala = table.Column<int>(nullable: false),
                    Termin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konsultacja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Konsultacja_AspNetUsers_PracownikUczelniId",
                        column: x => x.PracownikUczelniId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Konsultacja_PracownikUczelniId",
                table: "Konsultacja",
                column: "PracownikUczelniId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Konsultacja");
        }
    }
}
