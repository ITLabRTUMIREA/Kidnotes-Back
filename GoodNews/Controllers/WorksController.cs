using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GoodNews.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Responses;

namespace GoodNews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly GoodNewsContext context;
        private readonly IMapper mapper;

        public WorksController(GoodNewsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompactTask>>> GetMyWorks()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await context
                .Works
                .Where(w => w.WorkType == WorkType.Handle || w.WorkType == WorkType.ContentApproved)
                .Where(w => w.PublisherId == id)
                .ProjectTo<CompactTask>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        [Authorize(Roles = "publisher")]
        [HttpPost]
        public async Task<ActionResult<FullTask>> TakeWork([FromBody]int id)
        {
            var target = await context
                .Works
                .Where(w => w.WorkType == Shared.WorkType.News)
                .SingleOrDefaultAsync(n => n.Id == id);
            
            if (target == null)
                return NotFound();

            target.WorkType = Shared.WorkType.Handle;
            target.PublisherId = this.UserId();

            await context.SaveChangesAsync();

            return await context.Works.Where(w => w.Id == id).ProjectTo<FullTask>(mapper.ConfigurationProvider).SingleAsync();
        }

        [Authorize(Roles = "publisher")]
        [HttpPost("{id}/content")]
        public async Task<ActionResult> SendContent(
            int id,
            [FromBody]string fileName)
        {
            var userId = this.UserId();
            var work = await context
                .Works
                .Include(w => w.Content)
                .Where(w => w.Id == id)
                .Where(w => w.PublisherId == userId)
                .Where(w => w.WorkType == Shared.WorkType.Handle)
                .SingleOrDefaultAsync();
            if (work == null)
                return NotFound();

            var content = new Content
            {
                Approved = true,
                SendingDate = DateTime.UtcNow,
                FileName = fileName
            };
            work.WorkType = WorkType.ContentApproved;
            work.Content.Add(content);
            await context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "publisher")]
        [HttpPost("{id}/link/{linkTypeId}")]
        public async Task<ActionResult> SetLink(
            int id,
            int linkTypeId,
            [FromBody]string link)
        {
            var userId = this.UserId();
            var work = await context
                .Works
                .Include(w => w.PublishedResources)
                .Include(w => w.WantedRecources)
                .Where(w => w.Id == id)
                .Where(w => w.PublisherId == userId)
                .Where(w => w.WorkType == WorkType.ContentApproved)
                .SingleOrDefaultAsync();
            if (work == null)
                return NotFound();

            var target = work.PublishedResources.FirstOrDefault(r => r.SocialNetworkTypeId == linkTypeId);
            if (target == null)
            {
                target = new SocialNetworkLink { SocialNetworkTypeId = linkTypeId };
                work.PublishedResources.Add(target);
            }
            target.Value = link;

            if (work.WantedRecources.Count == work.PublishedResources.Count)
            {
                work.WorkType = WorkType.Published;
            }


            await context.SaveChangesAsync();
            return Ok();
        }
    }
}