using AutoMapper;
using Quartalsarbeit_GR1.Controllers.api;
using Quartalsarbeit_GR1.Dtos;
using Quartalsarbeit_GR1.Models;

namespace Quartalsarbeit_GR1.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            Mapper.CreateMap<Discipline, DisciplineDto>();
            Mapper.CreateMap<Category, CategoryDto>();
            Mapper.CreateMap<Configuration, ConfigurationDto>();
            Mapper.CreateMap<Event, EventDto>();
            Mapper.CreateMap<Club, ClubDto>();
            Mapper.CreateMap<Participant, ParticipantDto>();
            Mapper.CreateMap<Athlete, AthleteDto>();
            Mapper.CreateMap<ApplicationUser, VereinsverantwortlicherDto>();


            // Dto to Domain
            Mapper.CreateMap<DisciplineDto, Discipline>()
                .ForMember(c => c.ID, opt => opt.Ignore());
            Mapper.CreateMap<CategoryDto, Category>()
                .ForMember(c => c.ID, opt => opt.Ignore());
            Mapper.CreateMap<ConfigurationDto, Configuration>()
                .ForMember(c => c.ID, opt => opt.Ignore());
            Mapper.CreateMap<EventDto, Event>()
                .ForMember(c => c.ID, opt => opt.Ignore());
            Mapper.CreateMap<ClubDto, Club>()
                .ForMember(c => c.ID, opt => opt.Ignore());
            Mapper.CreateMap<ParticipantDto, Participant>()
                .ForMember(c => c.ID, opt => opt.Ignore());
            Mapper.CreateMap<AthleteDto, Athlete>()
                .ForMember(c => c.ID, opt => opt.Ignore());
            Mapper.CreateMap<VereinsverantwortlicherDto, ApplicationUser>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}