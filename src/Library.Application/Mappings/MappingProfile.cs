using AutoMapper;
using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Books, BookDto>();
            CreateMap<CreateBookDto, Books>();
            CreateMap<Loans, LoanDto>();
            CreateMap<CreateLoanDto, Loans>();
        }
    }
}