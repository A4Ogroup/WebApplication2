using Microsoft.AspNetCore.Components.Web;

namespace WebApplication2.Models.Repository
{
    public interface IReportRepository
    {
     
        Report GetById(int id);
        IEnumerable<Report> GetAll();
        Report Add(Report report);
        Report Update(Report report);
        void Delete(int id);
        bool Save();
    }
}
