using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using thethaomvc.Models;

namespace thethaomvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<thethaomvc.Models.SanPham> SanPham { get; set; }
        public DbSet<thethaomvc.Models.DanhGia> DanhGia { get; set; }
        public DbSet<thethaomvc.Models.BaiViet> BaiViet { get; set; }
        public DbSet<thethaomvc.Models.Bill> Bill { get; set; }
        public DbSet<thethaomvc.Models.BillDetail> BillDetail { get; set; }
    }
}
