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
    public class ProductImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductImages.Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // GET: ProductImages/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,FileName,FilePath,CreatedAt")] ProductImage productImage,IFormFile Picture)
        {

            if (ModelState.IsValid)
            {
                if(Picture!=null)
                {
                    if(Picture.Length>0)
                    {
                        var extension=Path.GetExtension(Picture.FileName);//uzanti yeni.jpg buradaki jpg
                        var name=Path.GetFileName(Picture.FileName);//dosyanın adını yeni
                        string yeni = Guid.NewGuid().ToString() + extension;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", yeni);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Picture.CopyTo(stream);
                        }
                        productImage.FilePath= yeni;
                        productImage.FileName= yeni;
                    }
                }


                _context.Add(productImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productImage.ProductId);
            return View(productImage);
        }

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productImage.ProductId);
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,FileName,FilePath,CreatedAt")] ProductImage productImage,IFormFile Picture)
        {
            if (id != productImage.Id)
            {
                return NotFound();
            }

           
                try
                {
                    if (Picture != null)
                    {
                        if (Picture.Length > 0)
                        {
                            
                            var extension = Path.GetExtension(Picture.FileName);//uzanti yeni.jpg buradaki jpg
                            var name = Path.GetFileName(Picture.FileName);//dosyanın adını yeni
                            string yeni = Guid.NewGuid().ToString() + extension;
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", yeni);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                Picture.CopyTo(stream);
                            }
                            productImage.FilePath = yeni;
                            productImage.FileName = yeni;
                        }
                    }
                    _context.Update(productImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImageExists(productImage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productImage.ProductId);
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage != null)
            {
                _context.ProductImages.Remove(productImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImages.Any(e => e.Id == id);
        }
    }
}
