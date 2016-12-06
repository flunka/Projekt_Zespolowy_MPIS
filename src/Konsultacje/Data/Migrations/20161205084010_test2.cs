using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Konsultacje.Data.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZapisNaKonsultacje",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<string>(nullable: true),
                    Temat = table.Column<string>(nullable: true),
                    ZapisID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZapisNaKonsultacje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ZapisNaKonsultacje_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZapisNaKonsultacje_Konsultacja_ZapisID",
                        column: x => x.ZapisID,
                        principalTable: "Konsultacja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZapisNaKonsultacje_StudentId",
                table: "ZapisNaKonsultacje",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ZapisNaKonsultacje_ZapisID",
                table: "ZapisNaKonsultacje",
                column: "ZapisID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZapisNaKonsultacje");
        }
    }
}
