using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GoodNews.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Identity;
using Shared.Responses;

namespace GoodNews.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public IdentityController(
            IMapper mapper,
            UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }


        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        public async Task<ActionResult<UserModel>> Me()
        {
            var finded = await userManager.FindByIdAsync(this.UserId().ToString());
            if (finded == null)
                return NotFound("wtf");
            var roles = await userManager.GetRolesAsync(finded);
            return new UserModel
            {
                Name  = finded.UserName,
                Roles = roles.ToList()
            };
        }


        [ProducesResponseType(400, Type = typeof(IEnumerable<IdentityError>))]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            var newUser = mapper.Map<User>(model);
            var result = await userManager.CreateAsync(newUser, model.Password);
            var roladd = await userManager.AddToRoleAsync(newUser, "viewer");
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(LoginResponse))]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound("no user");
            var check = await userManager.CheckPasswordAsync(user, model.Password);
            if (!check)
                return Unauthorized();


            return await GenerateResponse(user);
        }


        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(LoginResponse))]
        [Authorize(Roles = "viewer")]
        [HttpPost("verify")]
        public async Task<ActionResult<LoginResponse>> Verify([FromBody] string phoneNumber)
        {
            var user = await userManager.FindByIdAsync(this.UserId().ToString());
            if (user == null)
                return NotFound("no user");

            user.PhoneNumber = phoneNumber;

            await userManager.AddToRoleAsync(user, "publisher");
            return await GenerateResponse(user);
        }

        private async Task<ActionResult<LoginResponse>> GenerateResponse(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("very strong line");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new LoginResponse { Token = tokenString });
        }
    }
}