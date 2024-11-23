using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Flora.Data;
using Flora.Models;

namespace Flora.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly FloraUserContext _context;

        public RegisterModel(FloraUserContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string formUsername { get; set; }
        [BindProperty]
        public string formPassword { get; set; }
        [BindProperty]
        public string confirmPassword { get; set; }
        public string Msg { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(formUsername) || string.IsNullOrEmpty(formPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                Msg = "All fields are required.";
                return Page();
            }

            if (formPassword != confirmPassword)
            {
                Msg = "Passwords do not match.";
                return Page();
            }

            var existingUser = _context.User.FirstOrDefault(u => u.username == formUsername);
            if (existingUser != null)
            {
                Msg = "Username already exists.";
                return Page();
            }

            // Save the new user
            var newUser = new User
            {
                username = formUsername,
                password = formPassword // In production, hash passwords before saving
            };
            _context.User.Add(newUser);
            _context.SaveChanges();

            Msg = "Registration successful!";
            return RedirectToPage("Index");
        }
    }
}
