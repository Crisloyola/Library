namespace Library.Application.interfaces
{
    using Library.Application.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILoanService
    {
        Task<LoanDto> CreateLoanAsync(CreateLoanDto createLoanDto);
        Task<LoanDto> GetLoanByIdAsync(int id);
        Task<IEnumerable<LoanDto>> GetAllLoansAsync();
        Task<LoanDto> UpdateLoanAsync(int id, CreateLoanDto updateLoanDto);
        Task<bool> DeleteLoanAsync(int id);
        Task<LoanDto> SearchStudentNameAsync(string StudentName);
    }
    
}
