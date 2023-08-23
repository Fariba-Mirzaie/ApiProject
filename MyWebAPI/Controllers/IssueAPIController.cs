using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueAPIController : ControllerBase
    {
        private readonly MyContext _context;

        public IssueAPIController(MyContext myContext)
        {
            _context = myContext;
        }

        //public IEnumerable<Issue> GetIssues() 
        //{
        //    return _context.Issues.ToList();
        //}

        [HttpGet]
        public async Task<IEnumerable<Issue1>> Get()
        {
            return await _context.Issues.ToListAsync();
        }


        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIssueById(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);

        }


        [HttpPost]
        public async Task<IActionResult> CreateIssue(Issue1 issue)
        {
            await _context.AddAsync(issue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateIssue), new { id = issue.IssueId },issue);
        
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditIssue(int id , Issue1 issue)
        {
            if (id != issue.IssueId) return BadRequest();

             _context.Entry(issue).State = EntityState.Modified;
             await  _context.SaveChangesAsync();

            return NoContent();
        
        }


        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteIssue(int Id)
        {
            var issue = await _context.Issues.FindAsync(Id);
            if(issue == null)
                return NotFound() ;

            _context.Remove(issue);
           await _context.SaveChangesAsync();

            return NoContent();

        }


    }
}
