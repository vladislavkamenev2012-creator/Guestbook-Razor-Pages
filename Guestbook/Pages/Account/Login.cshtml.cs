using Guestbook.Models.ViewModels;
using Guestbook.Repositories;
using Guestbook.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Guestbook.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IRepository _repository;

        public LoginModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = _repository.GetUserByName(Input.Login);
            if (user != null && PasswordHasher.Verify(Input.Password, user.Pwd))
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.Name);
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
            return Page();
        }
    }
}
