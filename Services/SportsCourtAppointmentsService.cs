using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Migrations;
using Infrastructure.Repositories;

namespace Services
{

    public class SportsCourtAppointmentsService : ISportsCourtAppointmentsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        private readonly IFirebaseNotificationService _firebaseNotificationService;
        public SportsCourtAppointmentsService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager, IFirebaseNotificationService firebaseNotificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
            _firebaseNotificationService = firebaseNotificationService;
        }

        public async Task CreateSportsCourtAppointments(SportsCourtAppointmentsDto modelDto)
        {
            SportsCourtAppointments sportsCourtAppointments = _mapper.Map<SportsCourtAppointments>(modelDto);
            if (sportsCourtAppointments.Status == null)
            {
                sportsCourtAppointments.Status = SchedulingStatus.pending;
            }
            await _unitOfWork.SportsCourtAppointments.Add(sportsCourtAppointments);
            _unitOfWork.Save();
            if (sportsCourtAppointments.IdClient != null && sportsCourtAppointments.IdClient > 0)
            {
                Client client = await _unitOfWork.Client.GetById((int)sportsCourtAppointments.IdClient);
                if (client != null && !string.IsNullOrEmpty(client.TokenApp))
                {
                    await _firebaseNotificationService.SendNotificationAsync(
                         new NotificationRequest
                         {
                             IdClient= sportsCourtAppointments.IdClient,
                             DeviceToken = client.TokenApp,
                             Notification = new FirebaseNotification { Body = $"Seu agendamento é " +
                             $"{modelDto.InitialDate:dddd, dd/MM/yyyy 'às' HH:mm} até {modelDto.FinalDate:dd/MM/yyyy 'às' HH:mm}", Title = "ArenaX" }
                         }
                         );
                }
            }
        }

        public async Task<bool> DeleteSportsCourtAppointments(int SportsCourtAppointmentsId)
        {
            if (SportsCourtAppointmentsId > 0)
            {
                var SportsCourtAppointmentsDetail = await _unitOfWork.SportsCourtAppointments.GetById(SportsCourtAppointmentsId);
                if (SportsCourtAppointmentsDetail != null)
                {
                    _unitOfWork.SportsCourtAppointments.Delete(SportsCourtAppointmentsDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<SportsCourtAppointments?> GetSportsCourtAppointmentsById(int SportsCourtAppointmentsId)
        {
            if (SportsCourtAppointmentsId > 0)
            {
                return await _unitOfWork.SportsCourtAppointments.GetLinksById(SportsCourtAppointmentsId);
            }
            return null;
        }

        public async Task<bool> UpdateSportsCourtAppointments(SportsCourtAppointmentsDto SportsCourtAppointmentsParam)
        {
            if (SportsCourtAppointmentsParam != null)
            {
                var SportsCourtAppointments = await _unitOfWork.SportsCourtAppointments.GetById(SportsCourtAppointmentsParam.Id);
                if (SportsCourtAppointments != null)
                {
                    SportsCourtAppointments.InitialDate = SportsCourtAppointmentsParam.InitialDate;
                    SportsCourtAppointments.FinalDate = SportsCourtAppointmentsParam.FinalDate;
                    _unitOfWork.SportsCourtAppointments.Update(SportsCourtAppointments);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }
        public async Task<List<SportsCourtAppointments>?> GetAllSportsCourtAppointmentsByIdCourt(int id)
        {
            if (id > 0)
            {
                return await _unitOfWork.SportsCourtAppointments.GetAllSportsCourtAppointmentsByIdCourt(id);
            }
            return null;
        }
        public class OpeningHoursType
        {
            public string? Monday { get; set; }
            public string? Tuesday { get; set; }
            public string? Wednesday { get; set; }
            public string? Thursday { get; set; }
            public string? Friday { get; set; }
            public string? Saturday { get; set; }
            public string? Sunday { get; set; }
        }
        public class TimeSlot
        {
            public DateTime Init { get; set; }
            public DateTime Final { get; set; }
            public int IdCourt { get; set; }
            public SchedulingStatus Status { get; internal set; }
            public SportsCourtAppointments? SportsCourtAppointments { get; internal set; }
            public SportCourtDto? SportCurt { get; internal set; }
        }
        public class AvailableSlotsResult
        {
            public int IdCourt { get; set; }
            public List<TimeSlot> AvailableSlots { get; set; } = new List<TimeSlot>();
        }

        public async Task<ResponseData?> GetSchedulesCourt(RequestSchedulingsCourt requestSchedulingsCourt, int tenantID)
        {
            SportsCenterDto? sportsCenter = await _unitOfWork.SportsCenter.GetByIdAsync(tenantID);
            if (sportsCenter != null)
            {
                if (sportsCenter.OpeningHours == null)
                {
                    return new ResponseData
                    {
                        Message = "Não foi localizado horários de funcionamento do centro esportivo.",
                        Success = false,
                    };
                }

                DateTime currentDate = requestSchedulingsCourt.InitDate;
                DateTime endDate = requestSchedulingsCourt.FinalDate;

                // Configuração dos horários de funcionamento
                var openingHours = new OpeningHoursType
                {
                    Monday = sportsCenter.OpeningHours.Monday,
                    Tuesday = sportsCenter.OpeningHours.Tuesday,
                    Wednesday = sportsCenter.OpeningHours.Wednesday,
                    Thursday = sportsCenter.OpeningHours.Thursday,
                    Friday = sportsCenter.OpeningHours.Friday,
                    Saturday = sportsCenter.OpeningHours.Saturday,
                    Sunday = sportsCenter.OpeningHours.Sunday
                };

                var daysWithValidHours = new Dictionary<string, List<int>>();

                // Processar horários de funcionamento
                foreach (var prop in typeof(OpeningHoursType).GetProperties())
                {
                    string hoursString = prop.GetValue(openingHours)?.ToString();

                    if (string.IsNullOrEmpty(hoursString) || hoursString.Equals("Fechado", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var timeParts = hoursString.Split(new[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (timeParts.Length < 2 || !TimeSpan.TryParse(timeParts[0], out var startTime) ||
                       !TimeSpan.TryParse(timeParts[1], out var endTime))
                    {
                        Console.WriteLine($"Formato inválido em {prop.Name}: {hoursString}");
                        continue;
                    }

                    var hoursList = new List<int>();
                    for (int hour = startTime.Hours; hour < endTime.Hours; hour++)
                    {
                        hoursList.Add(hour);
                    }

                    daysWithValidHours.Add(prop.Name, hoursList);
                }

                // Buscar quadras e agendamentos
                List<SportsCourt>? courtList = await _unitOfWork.SportsCourt.GetAllByIdSportsCenter(tenantID);
                if (courtList != null)
                {
                    List<AvailableSlotsResult> result = new();

                    while (currentDate <= endDate)
                    {
                        string currentDay = currentDate.DayOfWeek.ToString();

                        if (!daysWithValidHours.TryGetValue(currentDay, out var availableHours))
                        {
                            currentDate = currentDate.AddDays(1);
                            continue;
                        }

                        var allAvailableSlots = new List<TimeSlot>();
                        foreach (var hour in availableHours)
                        {
                            foreach (var court in courtList)
                            {
                                var slotStart = currentDate.Date.AddHours(hour);
                                var slotEnd = slotStart.AddHours(1);

                                allAvailableSlots.Add(new TimeSlot
                                {
                                    Init = slotStart,
                                    Final = slotEnd,
                                    IdCourt = court.Id,

                                });
                            }
                        }

                        // Obter todos os agendamentos ocupados
                        var occupiedSlots = courtList
                            .SelectMany(c => c.SportsCourtAppointments)
                            .Where(a => a.InitialDate.Date == currentDate.Date)
                            .ToList();

                        // Criar lista completa com status para todos os slots
                        var allSlotsWithStatus = allAvailableSlots
                        .Select(available =>
                        {
                            var matchingOccupied = occupiedSlots
                                .Where(occupied => occupied.IdSportsCourt == available.IdCourt)
                                .FirstOrDefault(occupied =>
                                    (available.Init >= occupied.InitialDate && available.Init < occupied.FinalDate) ||
                                    (available.Final > occupied.InitialDate && available.Final <= occupied.FinalDate) ||
                                    (available.Init <= occupied.InitialDate && available.Final >= occupied.FinalDate));

                            SchedulingStatus status = matchingOccupied == null ?
                            SchedulingStatus.available :
                            matchingOccupied.Status == null ? SchedulingStatus.scheduled
                            : (SchedulingStatus)matchingOccupied.Status;

                            return new TimeSlot
                            {
                                IdCourt = available.IdCourt,
                                Init = available.Init,
                                Final = available.Final,
                                Status = status,
                                SportsCourtAppointments = matchingOccupied,
                                SportCurt = courtList.Where(x => x.Id == available.IdCourt).Select(x => new SportCourtDto
                                {
                                    Description = x.Description,
                                    Price = x.Price,
                                    Id = x.Id,
                                    SubName = x.SubName,
                                    IdSportsCenter = x.IdSportsCenter,
                                    Name = x.Name,
                                }).FirstOrDefault()
                            };
                        })
                        .ToList();

                        // Agrupar por quadra
                        // Agrupar por quadra (todos os slots - disponíveis e agendados)
                        var dailyResult = allSlotsWithStatus
                            .GroupBy(slot => slot.IdCourt)
                            .Select(group => new AvailableSlotsResult
                            {
                                IdCourt = group.Key,
                                //AllSlots = group.ToList(), // Lista completa com status
                                //AvailableSlots = group.Where(x => x.Status == "disponível").ToList(),
                                //BookedSlots = group.Where(x => x.Status == "agendado").ToList()
                                AvailableSlots = group.ToList()
                            })
                            .OrderBy(x => x.IdCourt)
                            .ToList();

                        result.AddRange(dailyResult);
                        currentDate = currentDate.AddDays(1);
                    }

                    return new ResponseData
                    {
                        Success = true,
                        Data = result
                    };
                }

                return new ResponseData
                {
                    Message = "Não localizado quadra desse centro esportivo",
                    Success = false,
                };
            }

            return new ResponseData
            {
                Message = "Não foi localizado o centro esportivo.",
                Success = false,
            };
        }
        public async Task<PagedResult<SportsCourtAppointments>> GetAllSportsCourtAppointments(ScheduleFilter scheduleFilter)
        {
            return await _unitOfWork.SportsCourtAppointments.GetAllSportsCourtAppointments(scheduleFilter);
        }
        public async Task CreateSportsCourtAppointmentsList(List<SportsCourtAppointmentsDto> value)
        {
            foreach (var item in value)
            {
                SportsCourtAppointments sportsCourtAppointments = _mapper.Map<SportsCourtAppointments>(item);
                if (sportsCourtAppointments.Status == null)
                {
                    sportsCourtAppointments.Status = SchedulingStatus.scheduled;
                }
                await _unitOfWork.SportsCourtAppointments.Add(sportsCourtAppointments);

            }
            _unitOfWork.Save();
        }
        public async Task<bool> CancelAppointment(int id)
        {
            SportsCourtAppointments value = await _unitOfWork.SportsCourtAppointments
                .GetById(id);
            value.Status = SchedulingStatus.canceled;
            _unitOfWork.SportsCourtAppointments.Update(value);
            _unitOfWork.Save();
            return true;
        }
        public async Task<bool?> AcceptAppointment(int id)
        {
            SportsCourtAppointments value = await _unitOfWork.SportsCourtAppointments
                .GetById(id);
            value.Status = SchedulingStatus.scheduled;
            _unitOfWork.SportsCourtAppointments.Update(value);
            _unitOfWork.Save();
            if (value.IdClient != null && value.IdClient > 0)
            {
                Client client = await _unitOfWork.Client.GetById((int)value.IdClient);
                if (client != null && !string.IsNullOrEmpty(client.TokenApp))
                {
                    await _firebaseNotificationService.SendNotificationAsync(
                         new NotificationRequest
                         {
                             IdClient = value.IdClient,
                             DeviceToken = client.TokenApp,
                             Notification = new FirebaseNotification
                             {
                                 Body = $"Seu agendamento foi confirmado: " +
                             $"{value.InitialDate:dddd, dd/MM/yyyy 'às' HH:mm} até {value.FinalDate:dd/MM/yyyy 'às' HH:mm}",
                                 Title = "ArenaX"
                             }
                         }
                         );
                }
            }
            return true;
        }
    }

    public interface ISportsCourtAppointmentsService
    {
        Task CreateSportsCourtAppointments(SportsCourtAppointmentsDto SportsCourtAppointments);
        Task<SportsCourtAppointments?> GetSportsCourtAppointmentsById(int SportsCourtAppointmentsId);
        Task<bool> UpdateSportsCourtAppointments(SportsCourtAppointmentsDto SportsCourtAppointmentsParam);
        Task<bool> DeleteSportsCourtAppointments(int SportsCourtAppointmentsId);
        Task<List<SportsCourtAppointments>?> GetAllSportsCourtAppointmentsByIdCourt(int id);
        Task<ResponseData?> GetSchedulesCourt(RequestSchedulingsCourt requestSchedulingsCourt, int tenantID);
        Task<PagedResult<SportsCourtAppointments>> GetAllSportsCourtAppointments(ScheduleFilter scheduleFilter);
        Task CreateSportsCourtAppointmentsList(List<SportsCourtAppointmentsDto> value);
        Task<bool> CancelAppointment(int id);
        Task<bool?> AcceptAppointment(int id);
    }

}
