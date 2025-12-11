namespace Library.Application.DTOs
{
    public class CreateLoanDto
    {
        public int BookId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;
        public bool Status { get; set; } = false;
    }
}