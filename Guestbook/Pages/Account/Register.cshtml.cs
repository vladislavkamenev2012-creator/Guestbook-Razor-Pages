using Guestbook.Models;
using Guestbook.Models.ViewModels;
using Guestbook.Repositories;
using Guestbook.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Guestbook.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IRepository _repository;

        public RegisterModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (_repository.UserExists(Input.Login))
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже существует");
                return Page();
            }

            var user = new User { Name = Input.Login, Pwd = PasswordHasher.Hash(Input.Password) };
            _repository.AddUser(user);

            return RedirectToPage("./Login");
        }
    }
}
