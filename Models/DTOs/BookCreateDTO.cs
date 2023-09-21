namespace MinimalAPI_BookStore.Models.DTOs
{
    public class BookCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}
