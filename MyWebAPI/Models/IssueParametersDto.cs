namespace MyWebAPI.Models
{
    public class IssueParametersDto
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public Issue.IssueType? Type { get; set; }
        public IssueSort? Sort { get; set; }
        public IssueSortDirection Direction { get; set; } = IssueSortDirection.Asc; 

    }
    public enum IssueSort
    {
        Id , Type
    }
    public enum IssueSortDirection
    {
        Asc,Desc
    }
}
