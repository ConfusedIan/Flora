using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Flora.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Flora.Data.FloraUserContext _context;

        public IndexModel(Flora.Data.FloraUserContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string formUsername { get; set; }
        [BindProperty]
        public string formPassword { get; set; }
        public string Msg { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var tempUser = _context.User.FirstOrDefault(u => u.username == formUsername && u.password == formPassword);
            if (tempUser != null)
            {
                HttpContext.Session.SetString("username", formUsername);
                return RedirectToPage("Home");
            }
            else
            {
                Msg = "Inavalid username or password.";
                return Page();
            }
        }

        public IActionResult OnGetLogout()
        {
            string cartKey = $"cart_{HttpContext.Session.GetString("username")}";

            // Clear session data
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove(cartKey);

            return RedirectToPage("Index");
        }


    }
}
