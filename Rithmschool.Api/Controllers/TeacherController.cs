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
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TeacherController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet(nameof(GetTeachers))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTeachers()
        {
            var teachers = _unitOfWork.teacherRepository.GetAll();
            if (teachers == null)
            {
                return NotFound("Not teachers");
            }
            var teachersDto = _mapper.Map<IEnumerable<TeacherDTO>>(teachers);
            return Ok(teachersDto);
        }


        [HttpGet(nameof(GetTeacher) + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _unitOfWork.teacherRepository.GetById(id);
            if (teacher == null)
            {
                return NotFound("Not teacher");
            }
            var courseDto = _mapper.Map<TeacherDTO>(teacher);
            return Ok(courseDto);
        }

        [HttpPost(nameof(PostTeacher))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostTeacher(TeacherDTO teacherDTO)
        {
            var teacher = _mapper.Map<Teacher>(teacherDTO);
            await _unitOfWork.teacherRepository.Add(teacher);
            await _unitOfWork.CommitAsync();
            return Ok(teacherDTO);
        }

        [HttpDelete(nameof(RemoveTeacher) + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveTeacher(int id)
        {
            var teacher = await GetTeacher(id);
            if (teacher == null)
            {
                return NotFound("Id not found");
            }
            await _unitOfWork.teacherRepository.Remove(id);
            await _unitOfWork.CommitAsync();
            return Ok("Deteled");
        }

        [HttpPut(nameof(UpdateCourse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCourse(TeacherDTO teacherDTO)
        {
            var teacher = _mapper.Map<Course>(teacherDTO);
            await _unitOfWork.courseRepository.Update(teacher);
            await _unitOfWork.CommitAsync();
            return Ok(teacherDTO);
        }
    }
}
