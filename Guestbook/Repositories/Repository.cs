using Guestbook.Data;
using Guestbook.Models;
using Microsoft.EntityFrameworkCore;

namespace Guestbook.Repositories
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Message> GetMessages()
        {
            return _context.Messages
                .Include(m => m.User)
                .OrderByDescending(m => m.MessageDate)
                .ToList();
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public User? GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Name == name);
        }

        public bool UserExists(string name)
        {
            return _context.Users.Any(u => u.Name == name);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
