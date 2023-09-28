using AutoMapper;
using Microsoft.AspNetCore.Builder;
using MinimalAPI_BookStore.Models;
using MinimalAPI_BookStore.Models.DTOs;
using MinimalAPI_BookStore.Repository;
using MinimalAPI_BookStore.Repository.IRepository;
using System.Net;

namespace MinimalAPI_BookStore.Endpoint
{
	public static class BookEndpoints
	{
		public static void ConfigureBookEndpoints(this WebApplication app)
		{
			app.MapGet("/api/books", GetAllBooks).WithName("GetBooks").Produces<APIResponse>(200);
			app.MapGet("/api/books/{id:int}", GetBook).WithName("Book").Produces<APIResponse>(200);
			app.MapPost("/api/AddBook", CreateBook).WithName("CreateBook").Accepts<BookCreateDTO>("application/json").Produces(201).Produces(400);
			app.MapPut("/api/UpdateBook", UpdateBook).WithName("UpdateBook").Accepts<BookUpdateDTO>("application/json").Produces<APIResponse>(200).Produces(400);
			app.MapDelete("/api/DeleteBook", DeleteBook).WithName("DeleteBook").Produces<APIResponse>(200);
		}

		private async static Task<IResult> GetAllBooks(IBookRepository _bookRepo)
		{
			APIResponse response = new();
			response.Result = await _bookRepo.GetAllAsync();
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.OK;

			return Results.Ok(response);
		}

		private async static Task<IResult> GetBook(IBookRepository _bookRepo, int id)
		{
			APIResponse response = new();
			response.Result = await _bookRepo.GetAsync(id);
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.OK;
			return Results.Ok(response);
		}

		private async static Task<IResult> CreateBook(IBookRepository _bookRepo, 
			IMapper _mapper, BookCreateDTO book_C_DTO)
		{
			APIResponse response = new() {IsSuccess = false, StatusCode = HttpStatusCode.BadRequest};
			if (_bookRepo.GetAsync(book_C_DTO.Title).GetAwaiter().GetResult() != null) {
				response.ErrorMessages.Add("Book title already exists.");
				return Results.BadRequest(response);
			}

			Book book = _mapper.Map<Book>(book_C_DTO);
			await _bookRepo.CreateAsync(book);
			await _bookRepo.SaveAsync();
			BookDTO _bookDTO = _mapper.Map<BookDTO>(book);

			response.Result = _bookDTO;
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.Created;
			return Results.Ok(response);
		}

		private async static Task<IResult> UpdateBook(IBookRepository _bookRepo,
			IMapper _mapper, BookUpdateDTO _book_U_DTO)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			await _bookRepo.UpdateAsync(_mapper.Map<Book>(_book_U_DTO));
			await _bookRepo.SaveAsync();

			response.Result = _mapper.Map<BookDTO>(await _bookRepo.GetAsync(_book_U_DTO.Id));
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.OK;
			return Results.Ok(response);
		}

		private async static Task<IResult> DeleteBook(IBookRepository _bookRepo, int id)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			Book bookFromDB = await _bookRepo.GetAsync(id);
			if (bookFromDB != null) {
				await _bookRepo.DeleteAsync(bookFromDB);
				await _bookRepo.SaveAsync();
				response.IsSuccess = true;
				response.StatusCode = HttpStatusCode.NoContent;
				return Results.Ok(response);
			} else {
				response.ErrorMessages.Add("Invalid ID");
				return Results.BadRequest(response);
			}
		}
	}
}
