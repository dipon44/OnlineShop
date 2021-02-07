using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;
using OnlineShop.Data;
using OnlineShop.Models.Product;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _hostingEnvironment;
        public ProductController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            this._db = db;
            this._hostingEnvironment = hostingEnvironment;
        }    
        
        public IActionResult ProductList()
        {
            return View(_db.products.Include(c=>c.ProductTypes).Include(f=>f.ProductTag).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.productTypes.ToList(),"Id", "ProductType");
            ViewData["productTagId"] = new SelectList(_db.productTags.ToList(), "Id", "ProductTagName");
            return View();
        }

        //Post Create Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(Products products, IFormFile image)
        {
            if(ModelState.IsValid)
            {
                if(image!=null)
                {
                    var name = Path.Combine(_hostingEnvironment.WebRootPath + "/Images",Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" +image.FileName;
                }
                _db.products.Add(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(ProductList));
            }
            return View(products);
        }
            
    }
}
