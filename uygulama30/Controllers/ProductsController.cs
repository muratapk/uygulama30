﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using uygulama30.Context;
using uygulama30.Models;

namespace uygulama30.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,CategoryId,StockQuantity,CreatedAt")] Product product,IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    if (picture.Length <= MaxFileSize)
                    {

                        var uzanti = Path.GetExtension(picture.FileName);
                        //bocek.png  .png domates.jpg  .jpg
                        string yeniisim = Guid.NewGuid().ToString() + uzanti;

                        string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/ProductImages/" + yeniisim);
                        using (var stream = new FileStream(yol, FileMode.Create))
                        {
                            picture.CopyToAsync(stream);
                        }
                        product.ProductPicture = yeniisim;
                    }
                }

                    _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,CategoryId,StockQuantity,CreatedAt")] Product product,IFormFile picture)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                try
                {
                    if (picture != null)
                    {
                        if (picture.Length <= MaxFileSize)
                        {

                            var uzanti = Path.GetExtension(picture.FileName);
                            //bocek.png  .png domates.jpg  .jpg
                            string yeniisim = Guid.NewGuid().ToString() + uzanti;

                            string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/ProductImages/" + yeniisim);
                            using (var stream = new FileStream(yol, FileMode.Create))
                            {
                                picture.CopyToAsync(stream);
                            }
                            product.ProductPicture = yeniisim;
                        }
                    }

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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var product = await _context.Products
                .Include(p => p.Category)
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
            var product = await _context.Products.FindAsync(id);
            
            if (product != null)
            {
                if(System.IO.File.Exists("/ProductImages/"+product.ProductPicture))
                {
                    System.IO.File.Delete("/ProductImages/" + product.ProductPicture);
                }
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
