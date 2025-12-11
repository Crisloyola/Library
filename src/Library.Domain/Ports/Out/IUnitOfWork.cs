namespace Library.Domain.Ports.Out
{
    public interface IUnitOfWOrk : IDisposable
    {
        IBookRepository Books { get; }
        ILoansRepository Loans { get; }
        Task<int> SaveChangesAsync();
    }

}