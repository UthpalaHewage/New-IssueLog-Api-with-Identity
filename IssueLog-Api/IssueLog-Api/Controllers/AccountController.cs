using IssueLog_Api.Models;
using IssueLog_Api.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace IssueLog_Api.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IssueLogDbContext _context;

        public AccountController()
        {

            _context = new IssueLogDbContext();
        }

        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(UserRegistrationModel model)
        {
            var userStore = new UserStore<ApplicationUser>(_context);
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
            user.FName = model.FName;
            user.LName = model.LName;

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3
            };

            string[] Roles = new string[1];

            Roles[0] = model.RoleName;


            IdentityResult result = manager.Create(user, model.Password);
            manager.AddToRoles(user.Id, Roles);
            return result;
        }

        [HttpPost]
        [Route("api/GetUserClaims")]
        [Authorize]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetUserClaimsAsync(GetUser user)
        {
            var userStore = new UserStore<ApplicationUser>(new IssueLogDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);

            ApplicationUser UserDetails = await manager.FindAsync(user.Email, user.Password);
            var userRoles = await manager.GetRolesAsync(UserDetails.Id);

            var model = new UserDetailModel
            {
                Id = UserDetails.Id,
                Email = UserDetails.Email,
                FName = UserDetails.FName,
                LName = UserDetails.LName,


                RoleName = userRoles[0]
            };



            return Ok(model);
        }

        [Route("api/AllClients")]
        public IHttpActionResult GetAllClient()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            var role = roleManager.FindByName("Client").Users.First();
            var usersInRole = _context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();

            var model = usersInRole.Select(user => new UserDetailModel
            {
                Id = user.Id,
                Email = user.Email,
                FName = user.FName,
                LName = user.LName,
                RoleName = "Client"

            });
            return Ok(model);
        }

        [Route("api/AllProjectManagers")]
        public IHttpActionResult GetAllProjectManagers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            var role = roleManager.FindByName("Project Manager").Users.First();
            var usersInRole = _context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();

            var model = usersInRole.Select(user => new UserDetailModel
            {
                Id = user.Id,
                Email = user.Email,
                FName = user.FName,
                LName = user.LName,
                RoleName = "Project Manager"

            });
            return Ok(model);
        }

        [Route("api/AllProjectLeader")]
        public IHttpActionResult GetAllProjectLeader()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            var role = roleManager.FindByName("Project Leader").Users.First();
            var usersInRole = _context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();

            var model = usersInRole.Select(user => new UserDetailModel
            {
                Id = user.Id,
                Email = user.Email,
                FName = user.FName,
                LName = user.LName,
                RoleName = "Project Leader"

            });
            return Ok(model);
        }
    }
}
