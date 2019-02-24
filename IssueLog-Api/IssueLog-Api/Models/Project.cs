using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueLog_Api.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ApplicationUser Client { get; set; }
        public string ClientId { get; set; }

        public virtual ApplicationUser ProjectManager { get; set; }
        public string ProjectManagerId { get; set; }

        public virtual ApplicationUser ProjectLeader { get; set; }
        public string ProjectLeaderId { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}