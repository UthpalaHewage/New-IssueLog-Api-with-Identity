using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssueLog_Api.Models
{
    public class ApplicationUser:IdentityUser
    {

        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }

        public virtual ICollection<Project> Client { get; set; }
        public virtual ICollection<Project> ProjectManager { get; set; }
        public virtual ICollection<Project> ProjectLeader { get; set; }
    }
}