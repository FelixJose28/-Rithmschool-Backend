using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rithmschool.Core.Entities;
using Rithmschool.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rithmschool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly RithmschoolContext _context;
        public WeatherForecastController(RithmschoolContext context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public IEnumerable<Course> Listado()
        {
            return _context.Courses.ToList();
        }
    }
}
