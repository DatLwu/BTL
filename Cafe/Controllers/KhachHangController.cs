using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cafe.Data;
using Cafe.Models;
using Cafe.Models.Process;
using OfficeOpenXml;
using X.PagedList;

namespace Cafe.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public KhachHangController(ApplicationDbcontext context)
        {
            _context = context;
        }
        private ExcelProcess _excelPro = new ExcelProcess();
        // GET: KhachHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.KhachHang.ToListAsync());
        }

        // GET: KhachHang/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang
                .FirstOrDefaultAsync(m => m.KhachHangID == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: KhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KhachHangID,KhachHangName,Address,SDT,Email")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }

        // GET: KhachHang/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("KhachHangID,KhachHangName,Address,SDT,Email")] KhachHang khachHang)
        {
            if (id != khachHang.KhachHangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.KhachHangID))
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
            return View(khachHang);
        }

        // GET: KhachHang/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang
                .FirstOrDefaultAsync(m => m.KhachHangID == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang != null)
            {
                _context.KhachHang.Remove(khachHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachHangExists(string id)
        {
            return _context.KhachHang.Any(e => e.KhachHangID == id);
        }
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file!=null)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    if (fileExtension != ".xls" && fileExtension != ".xlsx")
                    {
                        ModelState.AddModelError("", "Please choose excel file to upload!");
                    }
                    else
                    {
                        //rename file when upload to server
                        var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", "File" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond + fileExtension);
                        var fileLocation = new FileInfo(filePath).ToString();
                        if (file.Length > 0)
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                //save file to server
                                await file.CopyToAsync(stream);
                                //read data from file and write to database
                                var dt = _excelPro.ExcelToDataTable(fileLocation);
                                for(int i = 0; i < dt.Rows.Count; i++)
                                {
                                    var kh = new KhachHang();
                                    kh.KhachHangID =dt.Rows[i][0].ToString();
                                    kh.KhachHangName = dt.Rows[i][1].ToString();
                                    kh.Address = dt.Rows[i][2].ToString();
                                    kh.SDT = dt.Rows[i][3].ToString();
                                    kh.Email = dt.Rows[i][4].ToString();

                                    _context.Add(kh);
                                }
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
            
            return View();
        }  
         public IActionResult Download()
        {
            var fileName = "KhachHangList.xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                excelWorksheet.Cells["A1"].Value = "KhachHangID";
                excelWorksheet.Cells["B1"].Value = "KhachHangName";
                excelWorksheet.Cells["C1"].Value = "Address";
                excelWorksheet.Cells["D1"].Value = "SDT";
                excelWorksheet.Cells["E1"].Value = "Email";
                var khList = _context.KhachHang.ToList();
                excelWorksheet.Cells["A2"].LoadFromCollection(khList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
            }
        }  
        public async Task<IActionResult> Index(int? page, int? PageSize){
            ViewBag.PageSize= new List<SelectListItem>(){
                new SelectListItem() { Value="3", Text="3"},
                new SelectListItem() { Value="5", Text="5"},
                new SelectListItem() { Value="10", Text="10"},
                new SelectListItem() { Value="15", Text="15"},
                new SelectListItem() { Value="25", Text="25"},
                new SelectListItem() { Value="50", Text="50"},
            };
            int pagesize = (PageSize ?? 3);
            ViewBag.psize= pagesize;
            var model = _context.KhachHang.ToList().ToPagedList(page ?? 1, pagesize);
            return View(model);
        }
    }
}
