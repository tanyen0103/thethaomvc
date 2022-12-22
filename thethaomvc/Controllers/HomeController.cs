using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using thethaomvc.Data;
using thethaomvc.Models;

namespace thethaomvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index(string searchString)
        {
            if(!String.IsNullOrEmpty(searchString))
            {
                return View(await _context.SanPham.Where(m => m.HangTT.Contains(searchString)).ToListAsync());
            }
            else
            {
                return View(await _context.SanPham.ToListAsync());
            }

        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(b => b.DanhGias)
                .FirstOrDefaultAsync(m => m.IdSP == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        public IActionResult About()
        {
            return View();
        }

        // GET: Home/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSP,TenSP,HangTT,GiaTien,SoLuong,MoTaSP,HinhAnh")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSP,TenSP,HangTT,GiaTien,SoLuong,MoTaSP,HinhAnh")] SanPham sanPham)
        {
            if (id != sanPham.IdSP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.IdSP))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .FirstOrDefaultAsync(m => m.IdSP == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);
            _context.SanPham.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool SanPhamExists(int id)
        {
            return _context.SanPham.Any(e => e.IdSP == id);
        }

        // Các phương thức liên quan đến GIỎ HÀNG

        // Đọc danh sách CartItem từ session
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("shopcart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Lưu danh sách CartItem trong giỏ hàng vào session
        void SaveCartSession(List<CartItem> list)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(list);
            session.SetString("shopcart", jsoncart);
        }

        // Xóa session giỏ hàng
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("shopcart");
        }

        // Cho hàng vào giỏ
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _context.SanPham
                .FirstOrDefaultAsync(m => m.IdSP == id);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại");
            }
            var cart = GetCartItems();
            var item = cart.Find(p => p.SanPham.IdSP == id);
            if (item != null)
            {
                item.SoLuong++;
            }
            else
            {
                cart.Add(new CartItem() { SanPham = product, SoLuong = 1 });
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        // Chuyển đến view xem giỏ hàng
        public IActionResult ViewCart()
        {
            return View(GetCartItems());
        }

        //Xoa 1 mat hang khoi gio
        public IActionResult RemoveItem(int id)
        {

            var cart = GetCartItems();
            var item = cart.Find(p => p.SanPham.IdSP == id);
            if (item != null)
            {
                cart.Remove(item);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        //Cap nhat so luong gio hang
        public IActionResult UpdateItem(int id, int quantity)
        {

            var cart = GetCartItems();
            var item = cart.Find(p => p.SanPham.IdSP == id);
            if (item != null)
            {
                item.SoLuong = quantity;
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        public IActionResult CheckOut()
        {
            return View(GetCartItems()); //Trang web yeu cau nhap tt client ( tt khach  & tt mat hang)
        }

        // Lập hóa đơn: lưu hóa đơn, lưu chi tiết hóa đơn
        [HttpPost, ActionName("CreateBill")]
        public async Task<IActionResult> CreateBill(string cusName, string cusPhone, string cusAddress, int billTotal)
        {
            //1..Lưu hóa đơn
            var bill = new Bill();
            bill.Date = DateTime.Now;
            bill.CustomerName = cusName;
            bill.CustomerPhone = cusPhone;
            bill.CustomerAddress = cusAddress;
            bill.BillTotal = billTotal;
            // cập nhật tổng tiền hóa đơn 


            _context.Add(bill);
            await _context.SaveChangesAsync();

            //2. thêm chi tiết hóa đơn
            var cart = GetCartItems();

            int amount = 0;
            int total = 0;
            foreach (var i in cart)
            {
                var b = new BillDetail();
                b.BillId = bill.BillId;
                b.ProductId = i.SanPham.IdSP;
                amount = i.SanPham.GiaTien * i.SoLuong;
                total += amount;
                b.Price = i.SanPham.GiaTien;
                b.Quantity = i.SoLuong;
                b.Amount = amount;
                _context.Add(b);

            }


            await _context.SaveChangesAsync();

            //3.Xóa giỏ hàng
            ClearCart();

            return RedirectToAction(nameof(Thank));
        }

        public IActionResult Thank()
        {
            return View();
        }
    }
}
