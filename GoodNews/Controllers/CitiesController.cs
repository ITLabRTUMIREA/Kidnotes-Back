using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodNews.Database;
using Shared;
using Shared.Responses;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace GoodNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly GoodNewsContext _context;
        private readonly IMapper mapper;

        public CitiesController(GoodNewsContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompactCity>>> GetCities()
        {
            return await _context.Cities
                .OrderBy(c => c.Name)
                .ProjectTo<CompactCity>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
