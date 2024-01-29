using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudProduct.Models;
using CrudStudent.Data;

namespace CrudStudent.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly CrudStudentContext _context;

        public ProductsController(CrudStudentContext context, IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //return _context.Product != null ? 
            //            View(await _context.Product.ToListAsync()) :
            //            Problem("Entity set 'CrudStudentContext.Product'  is null.");

            List<Product> products = _context.Product.ToList();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Category = GetCategories();
            Product product = new Product();
            return View(product);
        }
        private List<SelectListItem> GetCategories()
        {
            var lstCategories = _context.Categories.Select(ct => new SelectListItem()
            {
                Value = ct.CtCode.ToString(),
                Text = ct.CatName
            }).ToList();

            lstCategories.Insert(0, new SelectListItem
            {
                Value = null,
                Text = "-----Select category-----"
            });

            return lstCategories;
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            string uniqueFileName = UploadedFile(product);
            string backFileName = GetBackImageFile(product);

            product.BackImageUrl = backFileName;
            product.ImageUrl = uniqueFileName;
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Added;
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private string UploadedFile(Product product)
        {
            string uniqueFileName = null;
            if (product.FrontImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.FrontImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.FrontImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string GetBackImageFile(Product product)
        {
            string uniqueFileName = null;
            if (product.BackImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.BackImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.BackImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Group,RollNumber,Name,Comment,Category,ImageUrl")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'CrudStudentContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
