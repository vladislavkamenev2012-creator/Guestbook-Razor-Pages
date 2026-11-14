using Guestbook.Models;

namespace Guestbook.Repositories
{
    public interface IRepository
    {
        List<Message> GetMessages();
        void AddMessage(Message message);
        User? GetUserByName(string name);
        bool UserExists(string name);
        void AddUser(User user);
    }
}
