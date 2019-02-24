using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueLog_Api.ViewModels.Project
{
    public class AddNewProjectModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClientId { get; set; }
        public string ProjectManagerId { get; set; }
        public string ProjectLeaderId { get; set; }
    }
}