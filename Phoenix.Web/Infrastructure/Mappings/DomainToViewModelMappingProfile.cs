using AutoMapper;
using Phoenix.Entities;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Web.Infrastructure.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
       // public override string ProfileName
       // {
       //     get
       //     {
       //         return "DomainToViewModelMappings";
       //     }
       ///}
        protected override void Configure()
        {
            Mapper.CreateMap<Movie, MovieViewModel>()
                .ForMember(vm => vm.Genre, map => map.MapFrom(m => m.Genre.Name))
                .ForMember(vm => vm.GenreId, map => map.MapFrom(m => m.Genre.ID))
                .ForMember(vm => vm.IsAvailable, map => map.MapFrom(m => m.Stocks.Any(s => s.IsAvailable)));

            Mapper.CreateMap<Genre, GenreViewModel>()
                .ForMember(vm => vm.NumberOfMovies, map => map.MapFrom(g => g.Movies.Count()));

            Mapper.CreateMap<Customer, CustomerViewModel>();

            Mapper.CreateMap<Stock, StockViewModel>();

            Mapper.CreateMap<Rental, RentalViewModel>();

            Mapper.CreateMap<Family, FamilyViewModel>();
        }
    }
}