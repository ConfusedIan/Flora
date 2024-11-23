using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flora.Helpers;
using Flora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Flora.Pages
{
    public class ShoppingModel : PageModel
    {
        private readonly Flora.Data.FloraContext _context;

        public ShoppingModel(Flora.Data.FloraContext context)
        {
            _context = context;
        }

        public string WelcomeUsername { get; set; } = string.Empty;

        public IList<Product> Product { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ProductCategory { get; set; }

        public async Task OnGetAsync()
        {
            // Get session username
            WelcomeUsername = HttpContext.Session.GetString("username") ?? "Guest";

            // Query for categories
            IQueryable<string> categoryQuery = from m in _context.Product
                                               orderby m.Category
                                               select m.Category;

            // Fetch products
            var products = from m in _context.Product
                           select m;

            // Filter by search string
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Name.Contains(SearchString) || s.Description.Contains(SearchString));
            }

            // Filter by product category
            if (!string.IsNullOrEmpty(ProductCategory))
            {
                products = products.Where(p => p.Category.Contains(ProductCategory));
            }

            // Populate categories
            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());

            // Get filtered products
            Product = await products.ToListAsync();
        }



        public IActionResult OnPostAddToCart(int id)
        {
            string cartKey = $"cart_{HttpContext.Session.GetString("username")}";
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, cartKey) ?? new List<Item>();

            var product = _context.Product.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToPage("Shopping");
            }

            int index = cart.FindIndex(i => i.Product.Id == id);
            if (index == -1)
            {
                cart.Add(new Item { Product = product, Quantity = 1 });
            }
            else
            {
                cart[index].Quantity++;
            }

            SessionHelper.SetObjectAsJson(HttpContext.Session, cartKey, cart);

            // Set TempData to pass the notification message
            TempData["Message"] = $"{product.Name} has been added to your cart.";

            return RedirectToPage();
        }


    }
}

