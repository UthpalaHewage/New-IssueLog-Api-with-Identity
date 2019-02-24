using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueLog_Api.ViewModels
{
    public class UserDetailModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string RoleName { get; set; }
    }
}