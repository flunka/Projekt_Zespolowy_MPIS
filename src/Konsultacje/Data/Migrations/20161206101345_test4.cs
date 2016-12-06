using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Konsultacje.Data.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Konsultacja_AspNetUsers_PracownikUczelniId",
                table: "Konsultacja");

            migrationBuilder.DropForeignKey(
                name: "FK_ZapisNaKonsultacje_AspNetUsers_StudentId",
                table: "ZapisNaKonsultacje");

            migrationBuilder.DropForeignKey(
                name: "FK_ZapisNaKonsultacje_Konsultacja_ZapisID",
                table: "ZapisNaKonsultacje");

            migrationBuilder.DropIndex(
                name: "IX_ZapisNaKonsultacje_ZapisID",
                table: "ZapisNaKonsultacje");

            migrationBuilder.DropColumn(
                name: "ZapisID",
                table: "ZapisNaKonsultacje");

            migrationBuilder.AddColumn<int>(
                name: "KonsultacjaID",
                table: "ZapisNaKonsultacje",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                nullable: true,
                computedColumnSql: "[Imie] + ' ' + [Nazwisko]");

            migrationBuilder.CreateIndex(
                name: "IX_ZapisNaKonsultacje_KonsultacjaID",
                table: "ZapisNaKonsultacje",
                column: "KonsultacjaID");

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Konsultacja_Pracownik",
                table: "Konsultacja",
                column: "PracownikUczelniId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Zapis_Konsultacja",
                table: "ZapisNaKonsultacje",
                column: "KonsultacjaID",
                principalTable: "Konsultacja",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Zapis_Student",
                table: "ZapisNaKonsultacje",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "ZapisNaKonsultacje",
                newName: "StudentID");

            migrationBuilder.RenameColumn(
                name: "PracownikUczelniId",
                table: "Konsultacja",
                newName: "PracownikUczelniID");

            migrationBuilder.RenameIndex(
                name: "IX_ZapisNaKonsultacje_StudentId",
                table: "ZapisNaKonsultacje",
                newName: "IX_ZapisNaKonsultacje_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Konsultacja_PracownikUczelniId",
                table: "Konsultacja",
                newName: "IX_Konsultacja_PracownikUczelniID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Konsultacja_Pracownik",
                table: "Konsultacja");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Zapis_Konsultacja",
                table: "ZapisNaKonsultacje");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Zapis_Student",
                table: "ZapisNaKonsultacje");

            migrationBuilder.DropIndex(
                name: "IX_ZapisNaKonsultacje_KonsultacjaID",
                table: "ZapisNaKonsultacje");

            migrationBuilder.DropColumn(
                name: "KonsultacjaID",
                table: "ZapisNaKonsultacje");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ZapisID",
                table: "ZapisNaKonsultacje",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZapisNaKonsultacje_ZapisID",
                table: "ZapisNaKonsultacje",
                column: "ZapisID");

            migrationBuilder.AddForeignKey(
                name: "FK_Konsultacja_AspNetUsers_PracownikUczelniId",
                table: "Konsultacja",
                column: "PracownikUczelniID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZapisNaKonsultacje_AspNetUsers_StudentId",
                table: "ZapisNaKonsultacje",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZapisNaKonsultacje_Konsultacja_ZapisID",
                table: "ZapisNaKonsultacje",
                column: "ZapisID",
                principalTable: "Konsultacja",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "ZapisNaKonsultacje",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "PracownikUczelniID",
                table: "Konsultacja",
                newName: "PracownikUczelniId");

            migrationBuilder.RenameIndex(
                name: "IX_ZapisNaKonsultacje_StudentID",
                table: "ZapisNaKonsultacje",
                newName: "IX_ZapisNaKonsultacje_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Konsultacja_PracownikUczelniID",
                table: "Konsultacja",
                newName: "IX_Konsultacja_PracownikUczelniId");
        }
    }
}
