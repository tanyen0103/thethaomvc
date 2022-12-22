using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace thethaomvc.Data.Migrations
{
    public partial class AddSanPham : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    IdSP = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSP = table.Column<string>(nullable: false),
                    HangTT = table.Column<string>(nullable: false),
                    GiaTien = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    MoTaSP = table.Column<string>(nullable: true),
                    HinhAnh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.IdSP);
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    IdDG = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    NgayDang = table.Column<DateTime>(nullable: false),
                    NoiDung = table.Column<string>(nullable: true),
                    IdSP = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.IdDG);
                    table.ForeignKey(
                        name: "FK_DanhGia_SanPham_IdSP",
                        column: x => x.IdSP,
                        principalTable: "SanPham",
                        principalColumn: "IdSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IdSP",
                table: "DanhGia",
                column: "IdSP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "SanPham");
        }
    }
}
