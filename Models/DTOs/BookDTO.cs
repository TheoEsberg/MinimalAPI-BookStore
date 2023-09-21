﻿namespace MinimalAPI_BookStore.Models.DTOs
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime? Year { get; set; }
        public bool IsAvailable { get; set; }
    }
}
