namespace Library.Domain.Entities
{
    public class Loans
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Status { get; set; } = false;
        public DateTime CreatedAt { get; set; }
    } 
}

