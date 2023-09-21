using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MinimalAPI_BookStore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Genre", "IsAvailable", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", "A story of the fabulously wealthy Jay Gatsby and his love for the beautiful Daisy Buchanan.", "Classic", true, "The Great Gatsby", new DateTime(1925, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Harper Lee", "The story of racial injustice and the loss of innocence in a Southern town.", "Fiction", true, "To Kill a Mockingbird", new DateTime(1960, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "George Orwell", "A dystopian novel set in a totalitarian society ruled by the Party and its leader, Big Brother.", "Science Fiction", true, "1984", new DateTime(1949, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Jane Austen", "The romantic entanglements of the Bennet sisters in early 19th century England.", "Romance", true, "Pride and Prejudice", new DateTime(1813, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "J.R.R. Tolkien", "The adventure of Bilbo Baggins as he sets out on a quest to help a group of dwarves reclaim their homeland.", "Fantasy", true, "The Hobbit", new DateTime(1937, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
