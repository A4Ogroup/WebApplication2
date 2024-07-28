
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models.Repository
{
    public class ReportRepository : IReportRepository
    {
        
        LconsultDBContext _context;
        public ReportRepository( LconsultDBContext context)
        {
            _context = context;
        }
        public Report Add(Report report)
        {
            _context.Reports.Add(report);
            _context.SaveChanges();
            return report;
        }

        public void Delete(int id)
        {
            var report = _context.Reports.FirstOrDefault(r => r.ReportId == id);
            _context.Reports.Remove(report);
            _context.SaveChanges();

        }

        public IEnumerable<Report> GetAll()
        {
            return _context.Reports;
        }
        public IEnumerable<Report> GetAllWithReview()
        {
            return _context.Reports.Include(r=>r.Review);
        }

        public Report GetById(int id)
        {
            return _context.Reports.FirstOrDefault(r =>r.ReportId == id);
             
        }

        public bool Save()
        {
            return (_context.SaveChanges() >=0);
        }

        public Report Update(Report report)
        {
            var _report= _context.Reports.Attach(report);
            _report.State=EntityState.Modified;
            return report;
           
        }

        async Task<IEnumerable<Report>> IReportRepository.GetAllAsync()
        {
            return await _context.Reports.ToListAsync();
        }
    }
}
