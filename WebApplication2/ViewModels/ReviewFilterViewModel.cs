namespace WebApplication2.ViewModels
{
    public class ReviewFilterViewModel
    {
    
            public List<double> Ratings { get; set; }
            public int PageNumber { get; set; } = 1;
            public string SortOrder { get; set; } // "newest" or "oldest"
    }
}
