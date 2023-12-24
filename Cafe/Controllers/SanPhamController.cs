using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cafe.Data;
using Cafe.Models;
using Cafe.Models.Process;
using OfficeOpenXml;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Cafe.Controllers

{
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbcontext _context;
        private ExcelProcess _excelPro = new ExcelProcess();

        public SanPhamController(ApplicationDbcontext context)
        {
            _context = context;
        }

        // GET: SanPham
        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
            ViewBag.pageSize =new List<SelectListItem>()
            {
                new SelectListItem() { Value="3", Text= "3"},
                new SelectListItem() { Value="5", Text= "5"},
                new SelectListItem() { Value="10", Text= "10"},
                new SelectListItem() { Value="15", Text= "15"},
                new SelectListItem() { Value="25", Text= "25"},
                new SelectListItem() { Value="50", Text= "50"},
            };
            int pagesize = (pageSize ?? 3 );
            ViewBag.psize = pageSize;

            var model = _context.SanPham.ToList().ToPagedList(page ?? 1, pagesize);
            return View(model);
        }

    

        // GET: SanPham/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .FirstOrDefaultAsync(m => m.SanPhamName == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPham/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SanPhamID,SanPhamName,SoLuong,Gia")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

        // GET: SanPham/Edit/5
        public async Task<IActionResult> Edit(string id)
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

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SanPhamID,SanPhamName,SoLuong,Gia")] SanPham sanPham)
        {
            if (id != sanPham.SanPhamName)
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
                    if (!SanPhamExists(sanPham.SanPhamName))
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

        // GET: SanPham/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .FirstOrDefaultAsync(m => m.SanPhamName == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPham.Remove(sanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(string id)
        {
            return _context.SanPham.Any(e => e.SanPhamName == id);
        }
        
        private bool HoaDonExists(string id)
        {
            return _context.HoaDon.Any(e => e.HoaDonID == id);
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
                        var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
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
                                    var hd = new HoaDon();
                                    hd.SanPhamName = dt.Rows[i][1].ToString();
                                    hd.SoLuong = dt.Rows[i][2].ToString();
                                    hd.Gia = dt.Rows[i][3].ToString();
                                    // hd.CreateDate = dt.Rows[i][6].ToString();

                                    _context.Add(hd);
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
                excelWorksheet.Cells["A1"].Value = "SanPhamName";
                excelWorksheet.Cells["B1"].Value = "SoLuong";
                excelWorksheet.Cells["C1"].Value = "Gia";
                var khList = _context.KhachHang.ToList();
                excelWorksheet.Cells["A2"].LoadFromCollection(khList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
            }
        }  
    }
   
}
