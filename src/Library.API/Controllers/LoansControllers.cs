using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly IUnitOfWOrk _unitOfWork;

        public LoansController(IUnitOfWOrk unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/loans
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _unitOfWork.Loans.GetAllAsync();
            return Ok(loans);
        }

        // GET: api/loans/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _unitOfWork.Loans.GetByIdAsync(id);
            if (loan == null)
                return NotFound("Loan not found");

            return Ok(loan);
        }

        // POST: api/loans
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Loans loan)
        {
            await _unitOfWork.Loans.AddAsync(loan);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
        }

        // PUT: api/loans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Loans loan)
        {
            var existing = await _unitOfWork.Loans.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Loan not found");

            existing.StudentName = loan.StudentName;
            existing.BookId = loan.BookId;
            existing.LoanDate = loan.LoanDate;
            existing.ReturnDate = loan.ReturnDate;

            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _unitOfWork.Loans.GetByIdAsync(id);
            if (loan == null)
                return NotFound("Loan not found");

            _unitOfWork.Loans.Remove(loan);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/loans/student/{name}
        [HttpGet("student/{name}")]
        public async Task<IActionResult> GetByStudent(string name)
        {
            var loans = await _unitOfWork.Loans.GetLoansByStudentNameAsync(name);
            return Ok(loans);
        }

        // GET: api/loans/overdue?currentDate=2025-01-01
        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdue([FromQuery] DateTime currentDate)
        {
            var overdue = await _unitOfWork.Loans.GetOverdueLoansAsync(currentDate);
            return Ok(overdue);
        }
    }
}
