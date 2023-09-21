# MinimalAPI-BookStore

MinimalAPI-BookStore is a .NET project that demonstrates the usage of the Minimal API framework to build a simple bookstore application. It implements CRUD (Create, Read, Update, Delete) operations for managing books and utilizes the Repository Pattern with Entity Framework for data access.

## Features

- **Minimal API Framework:** Utilizes ASP.NET Minimal API for lightweight and efficient API development.
- **CRUD Operations:** Demonstrates how to perform Create, Read, Update, and Delete operations on book records.
- **Repository Pattern:** Implements the Repository Pattern for a structured and maintainable data access layer.
- **Entity Framework:** Uses Entity Framework Core for database interactions.
- **Swagger Documentation:** Provides API documentation through Swagger UI for easy testing and exploration.

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/TheoEsberg/MinimalAPI-BookStore.git
```

### Navigate to the Project Directory

```bash
cd MinimalAPI-BookStore
```

### Restore Dependencies

```bash
dotnet restore
```

### Configure the Database

Edit the `appsettings.json` file to configure the database connection.

### Apply Database Migrations

```bash
dotnet ef database update
```

### Run the Application

```bash
dotnet run
```

The application should now be running locally.

## Usage

MinimalAPI-BookStore provides a simple API for managing books. You can interact with the API using tools like [Swagger UI](https://swagger.io/tools/swagger-ui/).

## API Endpoints

The following API endpoints are available:

- `GET /api/books`: Retrieve a list of all books.
- `GET /api/books/{id}`: Retrieve a book by ID.
- `POST /api/AddBook`: Create a new book.
- `PUT /api/UpdateBook/{id}`: Update a book by ID.
- `DELETE /api/DeleteBook/{id}`: Delete a book by ID.

For detailed API documentation and testing, you can access Swagger UI at `https://localhost:5001/swagger/index.html` when the application is running.

## Database Configuration

MinimalAPI-BookStore uses Entity Framework Core for database interactions. You can configure the database connection string in the `appsettings.json` file.

```json
"ConnectionStrings": {
  "Connection": "YourConnectionStringHere"
}
```
Make sure to update "YourConnectionStringHere" with your actual database connection string. If you miss this step the application will not run!

## Contributing
Contributions to this project are welcome! If you find issues or have suggestions for improvements, please feel free to open an issue or submit a pull request.

