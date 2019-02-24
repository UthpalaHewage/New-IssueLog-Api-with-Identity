using IssueLog_Api.Models;
using IssueLog_Api.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IssueLog_Api.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly IssueLogDbContext _context;

        public ProjectController()
        {
            _context = new IssueLogDbContext();
        }

        [HttpPost]
        public IHttpActionResult AddProject(AddNewProjectModel newProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var project = new Project
                {
                    Name = newProject.Name,
                    Description = newProject.Description,
                    ClientId = newProject.ClientId,
                    ProjectLeaderId = newProject.ProjectLeaderId,
                    ProjectManagerId = newProject.ProjectManagerId
                };

                _context.Projects.Add(project);
                _context.SaveChanges();

                return Ok(project);
            }

        }

        //get project By user
        [HttpGet]
        [Route("api/project/userproject/{userId}")]
        public IHttpActionResult GetProjectByUserId(string userId)
        {
            var projects = _context.Projects.Where(u => u.ClientId == userId || u.ProjectLeaderId == userId || u.ProjectManagerId == userId)
                .Include(p=>p.Client).Include(l => l.ProjectLeader).Include(m => m.ProjectManager);

            if (projects == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(projects.Select(project => new ProjectDetailModel
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    ClientId = project.Client.Id,
                    ClientName = project.Client.FName + " " + project.Client.LName,
                    ProjectLeaderId = project.ProjectLeaderId,
                    ProjectLeaderName = project.ProjectLeader.FName + " " + project.ProjectLeader.LName,

                    ProjectManagerId = project.ProjectManagerId,
                    ProjectManagerName = project.ProjectManager.FName + " " + project.ProjectManager.LName,
                }));
            }
        }
    }
}
