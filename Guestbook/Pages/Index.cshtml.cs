using System.ComponentModel.DataAnnotations;
using Guestbook.Models;
using Guestbook.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Guestbook.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public List<Message> Messages { get; private set; } = new();

        public bool IsAuthenticated { get; private set; }

        public string? UserName { get; private set; }

        [BindProperty]
        [Display(Name = "Введите сообщение")]
        [Required(ErrorMessage = "Введите текст сообщения")]
        public string? MessageText { get; set; }

        public void OnGet()
        {
            LoadState();
        }

        public IActionResult OnPostAddMessage()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Account/Login");
            }

            if (!ModelState.IsValid)
            {
                LoadState();
                return Page();
            }

            var msg = new Message
            {
                Id_User = userId.Value,
                MessageText = MessageText!,
                MessageDate = DateTime.Now
            };
            _repository.AddMessage(msg);

            return RedirectToPage();
        }

        private void LoadState()
        {
            Messages = _repository.GetMessages();
            IsAuthenticated = HttpContext.Session.GetInt32("UserId") != null;
            UserName = HttpContext.Session.GetString("UserName");
        }
    }
}
