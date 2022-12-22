using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace thethaomvc.Models
{
    public class BillDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillDetailId { get; set; }

        public int BillId { get; set; }

        public int ProductId { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public int Amount { get; set; }

        public Bill Bill { get; set; }

        public SanPham SanPham { get; set; }
    }
}
