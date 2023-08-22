namespace MyWebAPI.Models
{
    public class Issue
    {
        public int IssueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IssueType Type { get; set; }


        public enum IssueType
        {
           feature,bug
        }
        public enum Priority
        {
            low, medium, high
        }

    }
}
