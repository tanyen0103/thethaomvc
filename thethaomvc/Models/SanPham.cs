using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace thethaomvc.Models
{
    public class SanPham
    {
        [Key]
        public int IdSP { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm!!!")]
        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập hãng thể thao.")]
        [Display(Name = "Hãng thể thao")]
        public string HangTT { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        [Display(Name = "Giá tiền")]
        public int GiaTien { get; set; }

        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        public string MoTaSP { get; set; }

        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }

        public ICollection<DanhGia> DanhGias { get; set; }
    }
}
