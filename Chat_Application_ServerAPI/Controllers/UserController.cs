using Chat_Application_ServerAPI.Data;
using Chat_Application_ServerAPI.Data.Models;
using Chat_Application_ServerAPI.Data.Models.DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListPractice.Data.Services.JWT;

namespace Chat_Application_ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ChatDbContext dbContext;
        private readonly IJWTTokenService tokenService;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,ChatDbContext dbContext, IJWTTokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var userToCreate = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username,
                FullName = model.FullName,
                FirstName= model.FirstName,
                LastName= model.LastName
                
            };

            var result = await userManager.CreateAsync(userToCreate, model.Password);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var userFomDB = await userManager.FindByNameAsync(model.Username);

            if (userFomDB == null)
                return BadRequest();

            var result = await signInManager.CheckPasswordSignInAsync(userFomDB, model.Password, false);

            if (!result.Succeeded)
                return BadRequest();

            return Ok(new
            {
                result = result,
                username = userFomDB.UserName,
                fullname = userFomDB.FullName,
                email = userFomDB.Email,
                userid = userFomDB.Id,
                token = tokenService.GenerateToken(userFomDB)

            });

        }


        [HttpGet("{userid}")]
        public async Task<IActionResult> GetAll(string UserId)
        {
            
            List<UserListModel> userList = new();
            var ves = await dbContext.Users.ToListAsync();
            var res = ves.Where(n => n.Id != UserId);
            foreach (var item in res)
            {
                UserListModel user = new() { Fullname = item.FullName , Username = item.UserName };
                userList.Add(user);
            }
            
            return Ok(userList);

        }


    }
}
