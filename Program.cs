using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_BookStore;
using MinimalAPI_BookStore.Data;
using MinimalAPI_BookStore.Models;
using MinimalAPI_BookStore.Models.DTOs;
using MinimalAPI_BookStore.Validation;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the database connection service
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// End-points
// Read all books
app.MapGet("/api/books", async (AppDbContext dbContexts) =>
{
    APIResponse response = new APIResponse();

    try {
        var books = await dbContexts.Books.ToListAsync();
        response.IsSuccess = true;
        response.Result = books;
        response.StatusCode = HttpStatusCode.OK;
    } 
    catch (Exception ex) {
        response.IsSuccess = false;
        response.ErrorMessages.Add(ex.Message);
        response.StatusCode = HttpStatusCode.InternalServerError;
    }

    return Results.Ok(response);
}).WithName("GetBooks").Produces(200);

// Read book by id
app.MapGet("/api/books/{id:int}", async (int id, AppDbContext dbContexts) =>
{
    APIResponse response = new APIResponse();

    try {
        var book = await dbContexts.Books.FirstOrDefaultAsync(b => b.Id == id);
        response.IsSuccess = true;
        response.Result = book;
        response.StatusCode = HttpStatusCode.OK;
    } 
    catch (Exception ex) {
        response.IsSuccess = false;
        response.ErrorMessages.Add(ex.Message);
        response.StatusCode = HttpStatusCode.InternalServerError;
    }
    
    if (response.Result == null)
    {
        response.ErrorMessages.Add("Invalid Book Id!");
        response.IsSuccess = false;
        return Results.BadRequest(response);
    }

    return Results.Ok(response);

}).WithName("Book").Produces(200);

// Create a new book
app.MapPost("/api/AddBook", async (
    [FromServices] IValidator<BookCreateDTO> validator,
    [FromServices] IMapper _mapper,
    [FromBody] BookCreateDTO bookCreateDTO,
    AppDbContext dbContext) =>
{
    APIResponse response = new APIResponse();

    // DTO Check for the new book
    var validationResult = await validator.ValidateAsync(bookCreateDTO);
    if (!validationResult.IsValid) { return Results.BadRequest(response); }

    Book book = _mapper.Map<Book>(bookCreateDTO);
    //book.Id = (await dbContext.Books.OrderByDescending(b => b.Id).FirstOrDefaultAsync()).Id + 1;
    dbContext.Books.Add(book);
    dbContext.SaveChanges();

    BookDTO bookDTO = _mapper.Map<BookDTO>(book);

    response.Result = bookDTO;
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.Created;
    return Results.Ok(response);
}).WithName("CreateBook").Accepts<BookCreateDTO>("application/json").Produces<APIResponse>(201);

// Update a book
app.MapPut("/api/UpdateBook", async (
    [FromServices] IValidator<BookUpdateDTO> validator,
    [FromServices] IMapper _mapper,
    [FromBody] BookUpdateDTO bookUpdateDTO,
    AppDbContext dbContext,
    int id) =>
{
    APIResponse response = new APIResponse();

    // DTO Check for the updated book
    var validationResult = await validator.ValidateAsync(bookUpdateDTO); 
    if (!validationResult.IsValid) { return Results.BadRequest(response); }

    // Get book by id
    var book = await dbContext.Books.FindAsync(id);

    // Check if book exist
    if (book == null) { return Results.NotFound(book); }

    if (bookUpdateDTO.Author != "string") { book.Author =  bookUpdateDTO.Author; }
    if (bookUpdateDTO.Description != "string") { book.Description = bookUpdateDTO.Description; }
    if (bookUpdateDTO.Title != "string") { book.Title = bookUpdateDTO.Title; }
    if (bookUpdateDTO.Genre != "string") { book.Genre = bookUpdateDTO.Genre; }
    await dbContext.SaveChangesAsync();
    
    response.Result = book;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;
    return Results.Ok(response);
}).WithName("UpdateBook").Produces<APIResponse>(200);

// Delete a book
app.MapDelete("/api/DeleteBook", async (int id, AppDbContext dbContexts) =>
{
    APIResponse response = new APIResponse();

    dbContexts.Books.Remove(dbContexts.Books.First(b => b.Id == id));
    dbContexts.SaveChanges();

    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;
    return Results.Ok(response);
}).WithName("DeleteBook").Produces<APIResponse>(200);

// Runs the app, put last in code
app.Run();

