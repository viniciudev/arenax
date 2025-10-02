using AutoMapper;
using Core.DTOs;

namespace Core
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Add this mapping configuration
            CreateMap<UserDto, User>(); // From DTO to Entity
            CreateMap<User, UserDto>(); // And the reverse if needed
            CreateMap<SportCourtDto, SportsCourt>();
            CreateMap<SportsCategoryDto, SportsCategory>();
            CreateMap<SportsCourtOperationDto, SportsCourtOperation>();
            CreateMap<CourtEvaluationsDto, CourtEvaluations>();
            CreateMap<SportsCourtAppointmentsDto, SportsCourtAppointments>();
            CreateMap<SportsCenterDto, SportsCenter>();
            CreateMap<AddressDto, Address>();
            CreateMap<OpeningHoursDto, OpeningHours>();
            CreateMap<ClientDto, Client>();
            CreateMap<SportsRegistrationDto, SportsRegistration>();
            CreateMap<NotificationsDto, Notifications>();
            CreateMap<ClientEvaluationDto, ClientEvaluation>();
        }
    }
}
