using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsShoppingCart.Infrastructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly CmsShoppingCartContext context;
        public ProductsController(CmsShoppingCartContext context)
        {
            this.context = context;
        }
        // GET /admin/products
        public async Task<IActionResult> Index()
        {
            return View(await context.Products.OrderByDescending(x => x.Id).Include(x=>x.Category).ToListAsync());
        }

        // GET /admin/products/create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(x=>x.Sorting),"Id","Name");
            return View();
        }

        // POST /admin/pages/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                var slug = await context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Page already exists");
                    return View(page);
                }
                context.Add(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "The page has been added!";

                return RedirectToAction("Index");
            }
            return View(page);
        }
    }
}