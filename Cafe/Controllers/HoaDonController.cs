using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cafe.Data;
using Cafe.Models;

namespace Cafe.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public HoaDonController(ApplicationDbcontext context)
        {
            _context = context;
        }

        // GET: HoaDon
        public async Task<IActionResult> Index()
        {
            var applicationDbcontext = _context.HoaDon.Include(h => h.KhachHang).Include(h => h.NhanVien).Include(h => h.SanPham);
            return View(await applicationDbcontext.ToListAsync());
        }

        // GET: HoaDon/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.SanPham)
                .FirstOrDefaultAsync(m => m.HoaDonID == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: HoaDon/Create
        public IActionResult Create()
        {
            ViewData["KhachHangID"] = new SelectList(_context.KhachHang, "KhachHangID", "KhachHangID");
            ViewData["NhanVienID"] = new SelectList(_context.NhanVien, "NhanVienID", "NhanVienID");
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID");
            return View();
        }

        // POST: HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HoaDonID,KhachHangID,NhanVienID,SanPhamID,SoLuong,Gia,CreateDate")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KhachHangID"] = new SelectList(_context.KhachHang, "KhachHangID", "KhachHangID", hoaDon.KhachHangID);
            ViewData["NhanVienID"] = new SelectList(_context.NhanVien, "NhanVienID", "NhanVienID", hoaDon.NhanVienID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", hoaDon.SanPhamID);
            return View(hoaDon);
        }

        // GET: HoaDon/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["KhachHangID"] = new SelectList(_context.KhachHang, "KhachHangID", "KhachHangID", hoaDon.KhachHangID);
            ViewData["NhanVienID"] = new SelectList(_context.NhanVien, "NhanVienID", "NhanVienID", hoaDon.NhanVienID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", hoaDon.SanPhamID);
            return View(hoaDon);
        }

        // POST: HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("HoaDonID,KhachHangID,NhanVienID,SanPhamID,SoLuong,Gia,CreateDate")] HoaDon hoaDon)
        {
            if (id != hoaDon.HoaDonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.HoaDonID))
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
            ViewData["KhachHangID"] = new SelectList(_context.KhachHang, "KhachHangID", "KhachHangID", hoaDon.KhachHangID);
            ViewData["NhanVienID"] = new SelectList(_context.NhanVien, "NhanVienID", "NhanVienID", hoaDon.NhanVienID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", hoaDon.SanPhamID);
            return View(hoaDon);
        }

        // GET: HoaDon/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.SanPham)
                .FirstOrDefaultAsync(m => m.HoaDonID == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hoaDon = await _context.HoaDon.FindAsync(id);
            if (hoaDon != null)
            {
                _context.HoaDon.Remove(hoaDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(string id)
        {
            return _context.HoaDon.Any(e => e.HoaDonID == id);
        }
    }
}
