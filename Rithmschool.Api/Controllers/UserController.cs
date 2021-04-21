using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rithmschool.Core.DTOs;
using Rithmschool.Core.Entities;
using Rithmschool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rithmschool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetUsers))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsers()
        {
            var users = _unitOfWork.userRepository.GetAll();
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
            var user = await _unitOfWork.userRepository.GetById(id);
            if (user == null)
            {
                return NotFound("Not user");
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost(nameof(PostUser))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            await _unitOfWork.userRepository.Add(user);
            await _unitOfWork.CommitAsync();
            return Ok(userDTO);
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
            await _unitOfWork.userRepository.Remove(id);
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
    }
}
