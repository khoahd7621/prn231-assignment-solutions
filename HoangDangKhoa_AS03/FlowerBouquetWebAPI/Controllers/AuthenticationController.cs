using BusinessObjects;
using FlowerBouquetWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.impl;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlowerBouquetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomerRepository _repository = new CustomerRepository();
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model.Email.Equals(_configuration["Credentials:Email"]))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, _configuration["Credentials:Email"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, UserRoles.Admin)
                };

                var token = GetToken(authClaims);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            var user = _repository.GetCustomerByEmail(model.Email);
            if (user != null)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, UserRoles.Customer)
                };

                var token = GetToken(authClaims);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = _repository.GetCustomerByEmail(model.Email);
            if (model.Email.Equals(_configuration["Credentials:Email"]) || userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });

            Customer user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                CustomerName = model.CustomerName,
                City = model.City,
                Country = model.Country,
                Birthday = model.Birthday,
                PasswordHash = model.Password
            };
            _repository.SaveCustomer(user);
            return StatusCode(StatusCodes.Status201Created, new Response { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
