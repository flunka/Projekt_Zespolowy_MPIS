using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Konsultacje.Data.Migrations
{
    public partial class propozycjeKonsultacji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropozycjaKonsultacji",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PracownikUczelniID = table.Column<string>(nullable: true),
                    StudentID = table.Column<string>(nullable: true),
                    Temat = table.Column<string>(nullable: true),
                    Termin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropozycjaKonsultacji", x => x.ID);
                    table.ForeignKey(
                        name: "ForeignKey_Propozycja_Pracownik",
                        column: x => x.PracownikUczelniID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_Propozycja_Student",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropozycjaKonsultacji_PracownikUczelniID",
                table: "PropozycjaKonsultacji",
                column: "PracownikUczelniID");

            migrationBuilder.CreateIndex(
                name: "IX_PropozycjaKonsultacji_StudentID",
                table: "PropozycjaKonsultacji",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropozycjaKonsultacji");
        }
    }
}
