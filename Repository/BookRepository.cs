using Microsoft.EntityFrameworkCore;
using MinimalAPI_BookStore.Data;
using MinimalAPI_BookStore.Models;
using MinimalAPI_BookStore.Repository.IRepository;

namespace MinimalAPI_BookStore.Repository
{
	public class BookRepository : IBookRepository
	{
		private readonly AppDbContext _appDbContext;

        public BookRepository(AppDbContext appDbContext)
        {
			this._appDbContext = appDbContext;
        }

        public async Task CreateAsync(Book book)
		{
			_appDbContext.Books.Add(book);
		}

		public async Task DeleteAsync(Book book)
		{
			_appDbContext.Books.Remove(book);
		}

		public async Task<IEnumerable<Book>> GetAllAsync()
		{
			return await _appDbContext.Books.ToListAsync();
		}

		public async Task<Book> GetAsync(int id)
		{
			return await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Book> GetAsync(string title)
		{
			return await _appDbContext.Books.FirstOrDefaultAsync(b => b.Title.ToLower() == title.ToLower());
		}

		public async Task SaveAsync()
		{
			_appDbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(Book book)
		{
			_appDbContext.Books.Update(book);
		}
	}
}
