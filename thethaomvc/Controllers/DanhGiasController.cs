using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using thethaomvc.Data;
using thethaomvc.Models;

namespace thethaomvc.Controllers
{
    public class DanhGiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DanhGiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DanhGias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DanhGia.Include(d => d.SanPham);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DanhGias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _context.DanhGia
                .Include(d => d.SanPham)
                .FirstOrDefaultAsync(m => m.IdDG == id);
            if (danhGia == null)
            {
                return NotFound();
            }

            return View(danhGia);
        }

        // GET: DanhGias/Create
        public IActionResult Create()
        {
            ViewData["IdSP"] = new SelectList(_context.SanPham, "IdSP", "HangTT");
            return View();
        }

        // POST: DanhGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDG,Email,NgayDang,NoiDung,IdSP")] DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSP"] = new SelectList(_context.SanPham, "IdSP", "TenSP", danhGia.IdSP);
            return View(danhGia);
        }

        // GET: DanhGias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _context.DanhGia.FindAsync(id);
            if (danhGia == null)
            {
                return NotFound();
            }
            ViewData["IdSP"] = new SelectList(_context.SanPham, "IdSP", "TenSP", danhGia.IdSP);
            return View(danhGia);
        }

        // POST: DanhGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDG,Email,NgayDang,NoiDung,IdSP")] DanhGia danhGia)
        {
            if (id != danhGia.IdDG)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhGiaExists(danhGia.IdDG))
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
            ViewData["IdSP"] = new SelectList(_context.SanPham, "IdSP", "TenSP", danhGia.IdSP);
            return View(danhGia);
        }

        // GET: DanhGias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _context.DanhGia
                .Include(d => d.SanPham)
                .FirstOrDefaultAsync(m => m.IdDG == id);
            if (danhGia == null)
            {
                return NotFound();
            }

            return View(danhGia);
        }

        // POST: DanhGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhGia = await _context.DanhGia.FindAsync(id);
            _context.DanhGia.Remove(danhGia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhGiaExists(int id)
        {
            return _context.DanhGia.Any(e => e.IdDG == id);
        }
    }
}
