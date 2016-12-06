using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Konsultacje.Data.Migrations
{
    public partial class test7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrzegladajZapisyViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Budynek = table.Column<int>(nullable: false),
                    DispalyName2 = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Sala = table.Column<int>(nullable: false),
                    Temat = table.Column<string>(nullable: true),
                    Termin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzegladajZapisyViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrzegladajZapisyViewModel");
        }
    }
}
