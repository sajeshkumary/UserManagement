using ServiceBookingLibrary.Models;
using UserManagement.Models;

namespace UserManagement.Repo
{
    public interface IUser
    {
        public Task<User> AddNewUser(User u);
        public Task<User> UpdateUser(int userId,User u,string activity);
        public Task<List<User>> getAllUsers();
        public Task<User> getUser(int u);
        public Task<User> login(Login login);
    }
}
