using Domain.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Models;
using Service.ICustomServices;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueAPIController : ControllerBase
    {
        //private readonly MyContext _context;

        //public IssueAPIController(MyContext myContext)
        //{
        //    _context = myContext;
        //}

        //public IEnumerable<Issue> GetIssues() 
        //{
        //    return _context.Issues.ToList();
        //}

        //[HttpGet]
        //public async Task<IEnumerable<Issue>> Get()
        //{
        //    return await _context.Issues.ToListAsync();
        //}


        //[HttpGet("id")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetIssueById(int id)
        //{
        //    var issue = await _context.Issues.FindAsync(id);
        //    return issue == null ? NotFound() : Ok(issue);

        //}


        //[HttpPost]
        //public async Task<IActionResult> CreateIssue(Issue issue)
        //{
        //    await _context.AddAsync(issue);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(CreateIssue), new { id = issue.IssueId },issue);

        //}
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> EditIssue(int id , Issue issue)
        //{
        //    if (id != issue.IssueId) return BadRequest();

        //     _context.Entry(issue).State = EntityState.Modified;
        //     await  _context.SaveChangesAsync();

        //    return NoContent();

        //}


        //[HttpDelete("{Id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteIssue(int Id)
        //{
        //    var issue = await _context.Issues.FindAsync(Id);
        //    if(issue == null)
        //        return NotFound() ;

        //    _context.Remove(issue);
        //   await _context.SaveChangesAsync();

        //    return NoContent();

        //}



        private readonly ICustomService<Issue> _services;
        private readonly MyContext _myContext;
        public IssueAPIController(MyContext context, ICustomService<Issue> customService)
        {
            _myContext = context;
            _services = customService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var lst = _services.GetAll();
            return Ok(lst);
        }

        [HttpGet("id")]
        public Issue GetById(int id) 
        {
          return _services.GetById(id);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Issue issue)
        { 
          var newIssue= _services.Insert(issue);
            return newIssue ?  Ok(issue): BadRequest("Insert Not Done") ;
        }

        [HttpPut("id")]
        public IActionResult Update([FromBody] Issue issue , int id)
        {
            var currentissue = _services.GetById(id);
            if (currentissue != null)
            {
                _services.Update(issue);
                return Ok("Update Done");
            }
            else
                return NotFound("Your Object Not Found!");

        }

        [HttpDelete("id")]
        public IActionResult Delete(int id) 
        {
            var currentissue = _services.GetById(id);
            if (currentissue != null)
            {
                _services.Delete(id);
                return Ok("Delete Ok");
            }
            else
                return NotFound("Delete Not Ok!");
            }


    }
}
