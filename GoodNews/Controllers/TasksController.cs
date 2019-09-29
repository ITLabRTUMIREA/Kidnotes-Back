using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GoodNews.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Responses;

namespace GoodNews.Controllers
{
    [Authorize(Roles = "viewer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly GoodNewsContext context;
        private readonly IMapper mapper;

        public TasksController(GoodNewsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompactTask>>> GetAll()
        {
            return await context
                .Works
                .Where(w => w.WorkType == Shared.WorkType.News)
                .ProjectTo<CompactTask>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FullTask>> GetTask(int id)
        {
            var task = await context
                .Works
                .Where(w => w.WorkType == Shared.WorkType.News)
                .Where(w => w.Id == id)
                .ProjectTo<FullTask>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (task == null)
            {
                return NotFound();
            }

            return task;

        }
    }

}