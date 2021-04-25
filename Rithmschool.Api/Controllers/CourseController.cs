using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rithmschool.Core.DTOs;
using Rithmschool.Core.Entities;
using Rithmschool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IWebHostEnvironment _enviroment;
        public CourseController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment enviroment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _enviroment = enviroment;
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


        [HttpGet(nameof(GetCourse)+"/{id}")]
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

        [HttpPost(nameof(CreateCourse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCourse([FromForm]CourseDTO courseDTO)
        {
            var upload = courseDTO.File;
            var course = new Course();
            course.CourseId = 0;
            course.Title = courseDTO.Title;
            course.Route = courseDTO.Route;
            course.Duration = courseDTO.Duration;
            course.Price = courseDTO.Price;
            course.TeacherId = courseDTO.TeacherId;

            var fileName = Path.Combine(_enviroment.ContentRootPath, "archivos", upload.FileName);
            await upload.CopyToAsync(new FileStream(fileName, FileMode.Create));

            course.Route = upload.FileName;
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
