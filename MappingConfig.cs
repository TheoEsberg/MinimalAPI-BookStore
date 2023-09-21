using AutoMapper;
using MinimalAPI_BookStore.Models;
using MinimalAPI_BookStore.Models.DTOs;

namespace MinimalAPI_BookStore
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, BookCreateDTO>().ReverseMap();
            CreateMap<Book, BookUpdateDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
