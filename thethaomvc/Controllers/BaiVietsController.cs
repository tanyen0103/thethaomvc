using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using thethaomvc.Data;
using thethaomvc.Models;

namespace thethaomvc.Controllers
{
    public class BaiVietsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaiVietsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BaiViets
        public async Task<IActionResult> Index()
        {
            return View(await _context.BaiViet.ToListAsync());
        }

        // GET: BaiViets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViet
                .FirstOrDefaultAsync(m => m.IdBV == id);
            if (baiViet == null)
            {
                return NotFound();
            }

            return View(baiViet);
        }

        // GET: BaiViets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BaiViets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("IdBV,TieuDe,NgayDang,NoiDung,HinhAnh,Email")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                baiViet.HinhAnh = Upload(file);
                _context.Add(baiViet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baiViet);
        }

        // GET: BaiViets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViet.FindAsync(id);
            if (baiViet == null)
            {
                return NotFound();
            }
            return View(baiViet);
        }

        // POST: BaiViets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("IdBV,TieuDe,NgayDang,NoiDung,HinhAnh,Email")] BaiViet baiViet)
        {
            if (id != baiViet.IdBV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        baiViet.HinhAnh = Upload(file);
                    }
                    _context.Update(baiViet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiVietExists(baiViet.IdBV))
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
            return View(baiViet);
        }

        // GET: BaiViets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViet
                .FirstOrDefaultAsync(m => m.IdBV == id);
            if (baiViet == null)
            {
                return NotFound();
            }

            return View(baiViet);
        }

        // POST: BaiViets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baiViet = await _context.BaiViet.FindAsync(id);
            _context.BaiViet.Remove(baiViet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiVietExists(int id)
        {
            return _context.BaiViet.Any(e => e.IdBV == id);
        }

        public string Upload(IFormFile file)
        {
            string fn = null;

            if (file != null)
            {
                // Phát sinh tên mới cho file để tránh trùng tên
                fn = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\images\\{fn}"; // đường dẫn lưu file
                // upload file lên đường dẫn chỉ định
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return fn;
        }
    }
}
