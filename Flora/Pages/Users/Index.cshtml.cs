using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Flora.Data;
using Flora.Models;

namespace Flora.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly Flora.Data.FloraUserContext _context;

        public IndexModel(Flora.Data.FloraUserContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
