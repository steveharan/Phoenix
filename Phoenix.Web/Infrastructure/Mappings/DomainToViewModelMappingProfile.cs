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
            Mapper.CreateMap<Family, FamilyViewModel>()
                .ForMember(vm => vm.Persons, map => map.MapFrom(m => m.Persons.Count()))
                .ForMember(vm => vm.Ethnicity, map => map.MapFrom(m => m.Ethnicity.EthnicityName))
                .ForMember(vm => vm.Diagnosis, map => map.MapFrom(m => m.Diagnosis.Name))
                .ForMember(vm => vm.DiagnosisSubType, map => map.MapFrom(m => m.DiagnosisSubType.Name));

            Mapper.CreateMap<Person, PersonViewModel>()
                .ForMember(vm => vm.FamilyName, map => map.MapFrom(m => m.Family.FamilyName))
                .ForMember(vm => vm.Ethnicity, map => map.MapFrom(m => m.Ethnicity.EthnicityName))
                .ForMember(vm => vm.Diagnosis, map => map.MapFrom(m => m.Diagnosis.Name))
                .ForMember(vm => vm.DiagnosisSubType, map => map.MapFrom(m => m.DiagnosisSubType.Name));

            Mapper.CreateMap<PersonRelationship, PersonRelationshipViewModel>()
                .ForMember(vm => vm.PersonId, map => map.MapFrom(m => m.RelationshipFromPersonId))
                .ForMember(vm => vm.RelationshipTypeName, map => map.MapFrom(m => m.RelationshipType.RelationshipTypeName))
                .ForMember(vm => vm.RelationshipName, map => map.MapFrom(m => (m.relationWithPerson.FirstName + " " + m.relationWithPerson.SurName)))
                .ForMember(vm => vm.RelationWithPersonId, map => map.MapFrom(m => m.relationWithPerson.ID));

            Mapper.CreateMap<Ethnicity, EthnicityViewModel>();
            Mapper.CreateMap<RelationshipType, RelationshipTypeViewModel>();
            Mapper.CreateMap<Diagnosis, DiagnosisViewModel>();
            Mapper.CreateMap<DiagnosisSubType, DiagnosisSubTypeViewModel>();
            Mapper.CreateMap<FamilyTree, FamilyTreeViewModel>();

        }
    }
}