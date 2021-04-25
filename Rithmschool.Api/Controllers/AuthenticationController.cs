using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rithmschool.Core.DTOs;
using Rithmschool.Core.Entities;
using Rithmschool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rithmschool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthenticationController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet(nameof(GetUsers))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsers()
        {
            var users = _unitOfWork.authenticationRepository.GetAll();
            if (users == null)
            {
                return NotFound("Not users");
            }
            var buysDto = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(buysDto);
        }


        [HttpGet(nameof(GetUser) + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _unitOfWork.authenticationRepository.GetById(id);
            if (user == null)
            {
                return NotFound("Not user");
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }



        [HttpGet(nameof(GetUserByEmail) + "/{user}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByEmail(string user)
        {
            var userFind = await _unitOfWork.authenticationRepository.GetUserByEmail(user);
            if (userFind == null)
            {
                return NotFound("User not found");
            }
            var userDto = _mapper.Map<UserDTO>(userFind);
            return Ok(userDto);

        }


        [HttpPost(nameof(RegisterUser))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);



            var validateUser = await _unitOfWork.authenticationRepository.ValidateEmail(user);
            if (validateUser != null && validateUser.Email == user.Email)
            {
                return NotFound("This email is already registered.");
            }


            await _unitOfWork.authenticationRepository.Add(user);
            await _unitOfWork.CommitAsync();

            var userToken = GenerateToken(user);

            return Ok(new { token = userToken, user});
        }



        [HttpPost(nameof(Loggin))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Loggin(UserLoginCustom logginusuario)
        {
            //si el usuario es valido 
            var validation = await IsValidUser(logginusuario);
            if (validation.Item1 == false)
            {
                return NotFound("User or password incorrets");
            }
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token = token });
            }
            return NotFound("Ocurrio un error");
        }

        [HttpDelete(nameof(RemoveUser) + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var user = await GetUser(id);
            if (user == null)
            {
                return NotFound("Id not found");
            }
            await _unitOfWork.authenticationRepository.Remove(id);
            await _unitOfWork.CommitAsync();
            return Ok("Deteled");
        }

        [HttpPut(nameof(UpdateUser))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(UserDTO userDTO)
        {
            var user = _mapper.Map<Course>(userDTO);
            await _unitOfWork.courseRepository.Update(user);
            await _unitOfWork.CommitAsync();
            return Ok(userDTO);
        }




        private async Task<(bool, User)> IsValidUser(UserLoginCustom login)
        {
            var user = await _unitOfWork.authenticationRepository.Loggin(login);
            return (user != null, user);
        }


        private string GenerateToken(User user)
        {
            //Header
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signinCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signinCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            //Payload
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddHours(16)
            );
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
