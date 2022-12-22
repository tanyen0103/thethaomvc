using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace thethaomvc.Data.Migrations
{
    public partial class AddBaiViet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaiViet",
                columns: table => new
                {
                    IdBV = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(nullable: false),
                    NgayDang = table.Column<DateTime>(nullable: false),
                    NoiDung = table.Column<string>(nullable: true),
                    HinhAnh = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiViet", x => x.IdBV);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaiViet");
        }
    }
}
