using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using Library.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : Repository<Loans>, ILoansRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loans>> GetLoansByStudentNameAsync(string studentName)
        {
            return await _context.Loans
                .Where(l => l.StudentName.Contains(studentName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Loans>> GetOverdueLoansAsync(DateTime currentDate)
        {
            return await _context.Loans
                .Where(l =>
                    l.ReturnDate == null &&        // No ha sido devuelto
                    l.LoanDate < currentDate.AddDays(-7)) // ejemplo: más de 7 días prestado
                .ToListAsync();
        }
    }
}
