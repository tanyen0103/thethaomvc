using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace thethaomvc.Models
{
    public class BaiViet
    {
        [Key]
        public int IdBV { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }
        [Display(Name = "Ngày đăng")]
        public DateTime NgayDang { get; set; }
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }
        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }
        [Display(Name = "Email / Người đăng")]
        public string Email { get; set; }
    }
}
