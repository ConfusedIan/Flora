using System.Collections.Generic;
using System.Linq;
using Flora.Helpers;
using Flora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flora.Pages
{
    public class CartModel : PageModel
    {
        public List<Item> cart { get; set; }
        public double Total { get; set; }

        private readonly Flora.Data.FloraContext context;

        public CartModel(Flora.Data.FloraContext _context)
        {
            context = _context;
        }

        public void OnGet()
        {
            string cartKey = $"cart_{HttpContext.Session.GetString("username")}";
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, cartKey) ?? new List<Item>();
            Total = cart.Any() ? cart.Sum(i => (double)i.Product.Price * i.Quantity) : 0;
        }

        public IActionResult OnGetBuyNow(int id)
        {
            string cartKey = $"cart_{HttpContext.Session.GetString("username")}";
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, cartKey) ?? new List<Item>();

            var product = context.Product.SingleOrDefault(a => a.Id == id);
            if (product == null)
            {
                return RedirectToPage("Shopping");
            }

            int index = Exists(cart, id);
            if (index == -1)
            {
                cart.Add(new Item { Product = product, Quantity = 1 });
            }
            else
            {
                cart[index].Quantity++;
            }

            SessionHelper.SetObjectAsJson(HttpContext.Session, cartKey, cart);
            return RedirectToPage("cart");
        }

        public IActionResult OnGetDelete(int id)
        {
            string cartKey = $"cart_{HttpContext.Session.GetString("username")}";
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, cartKey) ?? new List<Item>();

            int index = Exists(cart, id);
            if (index != -1)
            {
                cart.RemoveAt(index);
            }

            SessionHelper.SetObjectAsJson(HttpContext.Session, cartKey, cart);
            return RedirectToPage("cart");
        }

        public IActionResult OnPostUpdate(Dictionary<int, int> quantities)
        {
            string cartKey = $"cart_{HttpContext.Session.GetString("username")}";
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, cartKey) ?? new List<Item>();

            foreach (var quantity in quantities)
            {
                int productId = quantity.Key;
                int updatedQuantity = quantity.Value;

                int index = Exists(cart, productId);
                if (index != -1)
                {
                    cart[index].Quantity = updatedQuantity;
                }
            }

            SessionHelper.SetObjectAsJson(HttpContext.Session, cartKey, cart);
            return RedirectToPage("cart");
        }

        private int Exists(List<Item> cart, int id)
        {
            return cart.FindIndex(i => i.Product.Id == id);
        }
    }
}
