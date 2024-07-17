namespace WebApplication2.Models
{
    public class UserInterests
    {
        
            public string  StudentId { get; set; }
            public int SubId { get; set; }

        public Student Student { get; set; } = null!;
        public SubCategory SubCategory { get; set; }=null!;
        
    }
}
