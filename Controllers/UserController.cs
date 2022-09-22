using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBookingLibrary.Models;
using UserManagement.Models;
using UserManagement.Repo;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TrainingServiceBookingContext _context;

        public UserController(TrainingServiceBookingContext db)
        {
            _context = db;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                UserRepo userRepo = new UserRepo(_context);
                return await userRepo.getAllUsers();
            }
            catch (NoUsersException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                UserRepo userRepo = new UserRepo(_context);

                return await userRepo.getUser(id);
            }
            catch (NoUsersException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> Add(User u)
        {
            UserRepo userRepo = new UserRepo(_context);

            return await userRepo.AddNewUser(u);
        }

        [CustomAuthFilter]
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody]User u)
        {
            try
            {
                UserRepo userRepo = new UserRepo(_context);

                return await userRepo.UpdateUser(id, u, "PROFILE");
            }
            catch (NoUsersException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [CustomAuthFilter]
        [Authorize]
        [HttpPut("ChangeRole/{id}")]
        public async Task<ActionResult<User>> UpdateRole(int id, User u)
        {
            try
            {
                UserRepo userRepo = new UserRepo(_context);

                return await userRepo.UpdateUser(id, u, "ROLE");
            }
            catch (NoUsersException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [CustomAuthFilter]
        [Authorize]
        [HttpPut("ChangePassword/{id}")]
        public async Task<ActionResult<User>> ChangePassword(int id, User u)
        {
            try
            {
                UserRepo userRepo = new UserRepo(_context);

                return await userRepo.UpdateUser(id, u, "PASSWORD");
            }
            catch (NoUsersException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(Login l)
        {
            try
            {
                UserRepo userRepo = new UserRepo(_context);

                return await userRepo.login(l);
            }
            catch (InvalidUser ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
