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
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseController(IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetCourses))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCourses()
        {
            var courses =  _unitOfWork.courseRepository.GetAll();
            if(courses == null)
            {
                return NotFound("Not courses");
            }
            var coursesDto = _mapper.Map<IEnumerable<CourseDTO>>(courses);
            return Ok(coursesDto);
        }


        [HttpGet(nameof(GetCourse)+"{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _unitOfWork.courseRepository.GetById(id);
            if(course == null)
            {
                return NotFound("Not course");
            }
            var courseDto = _mapper.Map<CourseDTO>(course);
            return Ok(courseDto);
        }

        [HttpPost(nameof(PostCourse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostCourse(CourseDTO courseDTO)
        {
            var course = _mapper.Map<Course>(courseDTO);
            await _unitOfWork.courseRepository.Add(course);
            await _unitOfWork.CommitAsync();
            return Ok(courseDTO);
        }

        [HttpDelete(nameof(RemoveCourse)+"/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCourse(int id)
        {
            var course = await GetCourse(id);
            if (course == null)
            {
                return NotFound("Id not found");
            }
            await _unitOfWork.courseRepository.Remove(id);
            await _unitOfWork.CommitAsync();
            return Ok("Deteled");
        }

        [HttpPut(nameof(UpdateCourse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCourse(CourseDTO courseDTO)
        {
            var course = _mapper.Map<Course>(courseDTO);
            await _unitOfWork.courseRepository.Update(course);
            await _unitOfWork.CommitAsync();
            return Ok(courseDTO);
        }
    }
}
