using AutoMapper;
using Library.Application.DTOs;
using Library.Application.interfaces;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;


namespace Library.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWOrk _unitOfWork;
        private readonly IMapper _mapper;

        public LoanService(IUnitOfWOrk unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LoanDto> CreateLoanAsync(CreateLoanDto createLoanDto)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(createLoanDto.BookId);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            var loan = _mapper.Map<Loans>(createLoanDto);
            await _unitOfWork.Loans.CreateAsync(loan);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<bool> DeleteLoanAsync(int id)
        {
            var loan = await _unitOfWork.Loans.GetByIdAsync(id);
            if (loan == null)
                throw new KeyNotFoundException("Loan not found.");

            var result = await _unitOfWork.Loans.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            var loans = await _unitOfWork.Loans.GetAllAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        public async Task<LoanDto> GetLoanByIdAsync(int id)
        {
            var loan = await _unitOfWork.Loans.GetByIdAsync(id);
            if (loan == null)
                throw new KeyNotFoundException("Loan not found.");

            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<LoanDto> SearchStudentNameAsync(string studentName)
        {
            var loan = (await _unitOfWork.Loans.GetLoansByStudentNameAsync(studentName)).FirstOrDefault();
            if (loan == null)
                throw new KeyNotFoundException("Loan not found.");

            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<LoanDto> UpdateLoanAsync(int id, CreateLoanDto updateLoanDto)
        {
            var existingLoan = await _unitOfWork.Loans.GetByIdAsync(id);
            if (existingLoan == null)
                throw new KeyNotFoundException("Loan not found.");

            _mapper.Map(updateLoanDto, existingLoan);
            await _unitOfWork.Loans.UpdateAsync(existingLoan);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<LoanDto>(existingLoan);
        }
    }
}
