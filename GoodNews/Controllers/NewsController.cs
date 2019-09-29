using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GoodNews.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Responses;

namespace GoodNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly GoodNewsContext context;
        private readonly IMapper mapper;

        public NewsController(GoodNewsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<CompactTask>>> GetAllAsync()
        {
            return await context.Works
                .Where(n => n.WorkType == Shared.WorkType.Published)
                .ProjectTo<CompactTask>(mapper.ConfigurationProvider)
                .ToListAsync(); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FullTask>> Get(int id)
        {
            int userId = -1;
            try
            {
                userId = this.UserId();
            }
            catch { }
            return await context.Works
                .Where(n => n.WorkType == Shared.WorkType.Published || (n.WorkType == Shared.WorkType.ContentApproved && n.PublisherId == userId))
                .Where(n => n.Id == id)
                .ProjectTo<FullTask>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
    }
}