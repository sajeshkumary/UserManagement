using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBookingLibrary.Models;
using UserManagement.Models;
using System.Linq;

namespace UserManagement.Repo
{

    public class UserRepo : IUser
    {
        private readonly TrainingServiceBookingContext _context;

        public UserRepo(TrainingServiceBookingContext db)
        {
            _context = db;
        }

        public async Task<User> AddNewUser(User u)
        {
            _context.Users.Add(u);
            await _context.SaveChangesAsync();
            return u;
        }

        public async Task<List<User>> getAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            if (users == null || users.Count() == 0)
            {
                throw new NoUsersException("No Users");
            }
            else
            {
                return users;
            }
        }

        public async Task<User> getUser(int uId)
        {
            var user = await _context.Users.FindAsync(uId);
            if (user == null)
            {
                throw new NoUsersException("No User with Search Critera");
            }
            else
            {
                return user;
            }
        }

        public async Task<User> login(Login login)
        {

            var user = await _context.Users.Where(x => x.Usename == login.UserName &&
                                x.Password == login.Password).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new InvalidUser("Invalid User");
            }
            else
            {
                return user;
            }
        }

        public async Task<User> UpdateUser(int userId, User u, string activity)
        {
            User usr = _context.Users.Find(userId);

            if (usr != null)
            {
                if (activity == "ROLE")
                {
                    usr.Role = u.Role;

                }
                else if (activity == "PASSWORD")
                {
                    usr.Password = u.Password;
                }
                else
                {
                    usr.Email = u.Email;
                    usr.Mobile = u.Mobile;
                    usr.Createddate = u.Createddate;
                    usr.Fullname = u.Fullname;
                }
                _context.Entry(usr).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return u;
            }
            else
            {
                throw new NoUsersException("No User with Search Critera");
            }

        }
    }
}
