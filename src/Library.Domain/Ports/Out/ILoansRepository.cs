using Library.Domain.Entities;

namespace Library.Domain.Ports.Out
{
    public interface ILoansRepository : IRepository<Loans>
    {
        Task<IEnumerable<Loans>> GetLoansByStudentNameAsync(string studentName);
        Task<IEnumerable<Loans>> GetOverdueLoansAsync(DateTime currentDate);
    }
}