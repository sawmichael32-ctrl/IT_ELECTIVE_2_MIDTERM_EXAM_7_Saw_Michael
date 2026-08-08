using IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Models;

namespace IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Repositories
{
    public class UserRepository
    {
        private static readonly List<User> users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@gym.com",
                Username = "admin",
                Password = "admin123"
            }
        };

        public List<User> GetAll()
        {
            return users;
        }

        public User? GetById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetByUsername(string username)
        {
            return users.FirstOrDefault(
                u => u.Username.Equals(
                    username,
                    StringComparison.OrdinalIgnoreCase)
            );
        }

        public User? ValidateLogin(string username, string password)
        {
            return users.FirstOrDefault(
                u => u.Username.Equals(
                    username,
                    StringComparison.OrdinalIgnoreCase)
                && u.Password == password
            );
        }

        public void Add(User user)
        {
            user.Id = users.Count == 0
                ? 1
                : users.Max(u => u.Id) + 1;

            users.Add(user);
        }
    }
}