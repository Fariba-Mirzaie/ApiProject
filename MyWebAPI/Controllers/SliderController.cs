using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly MyContext _context;
        public SliderController(MyContext myContext)
        {
            _context = myContext;
        }

        [HttpGet]
        public async Task<List<Issue>> Get([FromQuery] IssueParametersDto parametes)
        {
            int skip = (parametes.Page - 1) * parametes.PerPage;

            var query = _context.Issues.AsQueryable();
            if (parametes.Type != null)
            {
                query = query.Where(p => p.Type == parametes.Type);
            }

            //if (parametes.Sort == IssueSort.Id)
            //{
            //    if (parametes.Direction == IssueSortDirection.Asc)
            //    {
            //        query = query.OrderBy(p=>p.IssueId);
            //    }
            //    else {
            //        query = query.OrderByDescending(p => p.IssueId);
            //    }

            //}


            //query = parametes.Direction switch
            //{
            //    IssueSortDirection.Asc => 
            //    parametes.Sort== IssueSort.Id ? query.OrderBy(p => p.IssueId) : query.OrderByDescending(p => p.IssueId)
            //    ,
            //    IssueSortDirection.Desc => query.OrderBy(p => p.Description)

            //};

            query = parametes.Sort switch
            {
                IssueSort.Id => parametes.Direction == IssueSortDirection.Asc ?
                query.OrderBy(p => p.IssueId) : //if
                query.OrderByDescending(p => p.IssueId), //else

                IssueSort.Type => parametes.Direction == IssueSortDirection.Asc ?
                query.OrderBy(p => p.Type) :
                query.OrderByDescending(p => p.Type),

                _ => parametes.Direction == IssueSortDirection.Asc ? //default
                query.OrderBy(p => p.Title) :
                query.OrderByDescending(p => p.Title)
            };


            return await query.Skip(skip)
                .Take(parametes.PerPage)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<Issue> Create(Issue issue)
        {
            var newIssue = await _context.Issues.AddAsync(issue);
            await _context.SaveChangesAsync();
            return issue;

        }

        [HttpPut("id")]
        public async Task<IActionResult> Edit(int id, [FromBody] Issue issue)
        {
            var currentissue = await _context.Issues.FindAsync(id);
            if (currentissue != null)
            {
                currentissue.Title = issue.Title;
                currentissue.Description = issue.Description;
                currentissue.Type = issue.Type;
                _context.Issues.Update(currentissue);
                await _context.SaveChangesAsync();
            }
            else 
                return NotFound();  

            return Ok();
        }

        [HttpDelete("id")]
        public async Task<string> Delete(int id)
        {
            var issue =await _context.Issues.FindAsync(id);
            if (issue != null)
            {
                _context.Issues.Remove(issue);
                await _context.SaveChangesAsync();
                return "Deleted OK";
            }
            else
                return "Delete Not Okk";
        
        }

    }
}
