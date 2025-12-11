namespace Library.Application.DTOs
{
    public class CreateLoanDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public bool status { get; set; } = false;
    }
}