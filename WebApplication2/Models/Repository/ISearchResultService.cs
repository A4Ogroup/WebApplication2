namespace WebApplication2.Models.Repository
{
    public interface ISearchResultService
    {
        IQueryable<Course> Courses { get; set; }
    }
}
