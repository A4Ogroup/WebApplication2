namespace WebApplication2.ViewModels.InstructorViewModels
{
    public class InstructorProfessionViewModel
    {
        public string Profession { get; set; }
        public byte YearsExperince { get; set; }
        public string? About { get; set; }

        public List<string>? SocialMediaAccounts { get; set; } = new List<string>{"","",""};


    }
}
