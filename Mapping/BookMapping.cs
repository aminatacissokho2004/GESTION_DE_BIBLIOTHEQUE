using AutoMapper;
using Gestion_de_Bibliothéque.DTOs;

namespace Gestion_de_Bibliothéque.Mapping
{
    public class BookMapping: Profile
    {
        public BookMapping()
        {
            //CreateMap<Book, BookDTOs>();
            CreateMap<Book, CreateBookDTOs>().ReverseMap();  // reverseMap fait le maping inverse
            CreateMap<CreateBookDTOs, Book>().ReverseMap();
            CreateMap<Book, BookDTOs>().ReverseMap();
            CreateMap<Book, UpdateBookDTOs>().ReverseMap();
        }
    }
} 
