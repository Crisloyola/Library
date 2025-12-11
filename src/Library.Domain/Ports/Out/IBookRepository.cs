using Library.Domain.Entities;
namespace Library.Domain.Ports.Out
{
    public interface IBookRepository : IRepository<Books>
    {
        Task<Books?> GetBookWithLoansAsync(int id);
        Task<IEnumerable<Books>> GetBooksByAuthorAsync(string author);
        Task<Books?> SearchBookNameAsync();
    }
}