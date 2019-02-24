using IssueLog_Api.Models;
using IssueLog_Api.ViewModels.Issue;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IssueLog_Api.Controllers
{
    public class IssueController : ApiController
    {
        private readonly IssueLogDbContext _context;

        public IssueController()
        {
            _context = new IssueLogDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var issue = _context.Issues.FirstOrDefault(u => u.Id == id);
            if (issue == null)
            {
                return NotFound();
            }
            else
            {
                var model = new IssueDetailsModel
                {
                    Id = issue.Id,
                    Title = issue.Title,
                    Description = issue.Description,
                    CriticalLevel = issue.CriticalLevel == true ? "Critical" : "Not Critical",
                    DeadLine = issue.DeadLine,
                    ProjectId = issue.ProjectId,
                    ProjectName = issue.Project.Name
                    // project = issue.Project
                };

                return Ok(model);
            }
        }


        [HttpPost]
        public IHttpActionResult AddIssue(AddIssueModel newIssue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var issue = new Issue
                {
                    Title = newIssue.Title,
                    Description = newIssue.Description,
                    CriticalLevel = newIssue.CriticalLevel,
                    DeadLine = newIssue.DeadLine,
                    ProjectId = newIssue.ProjectId
                };

                _context.Issues.Add(issue);
                _context.SaveChanges();

                return Ok(issue);
            }
        }

        [HttpGet]
        [Route("api/issue/projectissues/{id}")]

        public IHttpActionResult IssuesByProjectId(int id)
        {
            var issues = _context.Issues.Where(i => i.ProjectId == id).Include(p => p.Project).ToList();

            if (issues == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(issues.Select(issue => new IssueDetailsModel
                {
                    Id = issue.Id,
                    Title = issue.Title,
                    Description = issue.Description,
                    CriticalLevel = issue.CriticalLevel == true ? "Critical" : "Not Critical",
                    DeadLine = issue.DeadLine,
                    ProjectId = issue.ProjectId,
                    ProjectName = issue.Project.Name
                    // project = issue.Project
                }));
            }

        }


    }
}
