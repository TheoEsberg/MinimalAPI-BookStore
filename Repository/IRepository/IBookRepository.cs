using MinimalAPI_BookStore.Models;

namespace MinimalAPI_BookStore.Repository.IRepository
{
	public interface IBookRepository
	{
		Task<IEnumerable<Book>> GetAllAsync();
		Task<Book> GetAsync(int id);
		Task<Book> GetAsync(string title);
		Task CreateAsync(Book book);
		Task UpdateAsync(Book book);
		Task DeleteAsync(Book book);
		Task SaveAsync();
	}
}
