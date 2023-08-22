using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAPI.Client
{
    internal class IssueDTO
    {
        public int IssueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IssueType Type { get; set; }


        public enum IssueType
        {
            feature, bug
        }
        public enum Priority
        {
            low, medium, high
        }

    }
}
