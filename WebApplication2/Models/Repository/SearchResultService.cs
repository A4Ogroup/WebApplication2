namespace WebApplication2.Models.Repository
{
    public class SearchResultService:ISearchResultService
    {
        public IQueryable<Course> Courses { get; set; }
    }
}
