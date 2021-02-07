using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models.Product;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTagController : Controller
    {
        private ApplicationDbContext db;

        public ProductTagController(ApplicationDbContext _db)
        {
            this.db = _db;
        }


        public IActionResult ProductTags()
        {
            var data = db.productTags.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTag productTags)
        {
            if (ModelState.IsValid)
            {
                db.productTags.Add(productTags);
                await db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(ProductTags));
            }
            return View(productTags);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = db.productTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTag productTags)
        {
            if (ModelState.IsValid)
            {
                db.Update(productTags);
                await db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(ProductTags));
            }
            return View(productTags);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = db.productTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTag productTags)
        {
            return RedirectToAction(actionName: nameof(ProductTags));
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = db.productTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTag productTag)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != productTag.Id)
            {
                return NotFound();
            }
            var pproductTag = db.productTags.Find(id);
            if (pproductTag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Remove(pproductTag);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(ProductTags));

            }
            return View(productTag);
        }
    }
}
