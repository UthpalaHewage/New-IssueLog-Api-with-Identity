using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueLog_Api.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool CriticalLevel { get; set; }
        public DateTime DeadLine { get; set; }

        public virtual Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}