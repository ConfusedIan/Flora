using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Flora.Data;
using Flora.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Flora.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly Flora.Data.FloraContext _context;

        public IndexModel(Flora.Data.FloraContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ProductCategory { get; set; }

        public async Task OnGetAsync()
        {
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
                products = products.Where(x => x.Category == ProductCategory);
            }

            // Populate Categories
            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
            Product = await products.ToListAsync();
        }
    }
}
