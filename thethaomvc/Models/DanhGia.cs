using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace thethaomvc.Models
{
    public class DanhGia
    {
        [Key]
        public int IdDG { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Ngày đăng")]
        public DateTime NgayDang { get; set; }

        [Display(Name = "Nội dung đánh giá")]
        public string NoiDung { get; set; }

        [Display(Name = "ID Sản Phẩm")]

        public int IdSP { get; set; }
        [ForeignKey("IdSP")]
        public SanPham SanPham { get; set; }
    }
}
