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
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext applicationDbContext;

        public ProductTypesController(ApplicationDbContext _applicationDbContext)
        {
            this.applicationDbContext = _applicationDbContext;
        }
        //ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        public IActionResult ProductType()
        {
            var data = applicationDbContext.productTypes.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if(ModelState.IsValid)
            {
                applicationDbContext.productTypes.Add(productTypes);
                await applicationDbContext.SaveChangesAsync();
                TempData["save"] = "Product Type Saved.";
                return RedirectToAction(actionName:nameof(ProductType));
            }
            return View(productTypes);
        }

        public IActionResult Edit(int ? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var productType = applicationDbContext.productTypes.Find(id);
            if(productType==null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                applicationDbContext.Update(productTypes);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(ProductType));
            }
            return View(productTypes);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = applicationDbContext.productTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
                return RedirectToAction(actionName: nameof(ProductType));
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = applicationDbContext.productTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {
            if(id==null)
            {
                return NotFound();
            }

            if(id!=productTypes.Id)
            {
                return NotFound();
            }
            var productType = applicationDbContext.productTypes.Find(id);
            if(productType==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                applicationDbContext.Remove(productType);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(ProductType));

            }
            return View(productTypes);
        }

    }
}
