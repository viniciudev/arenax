using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{


    public class SportsCourtAppointmentsRepository : GenericRepository<SportsCourtAppointments>, ISportsCourtAppointmentsRepository
    {
        private readonly IImageUrlRepository _imageUrlRepository;
        public SportsCourtAppointmentsRepository(DbContextClass dbContext, IImageUrlRepository imageUrlRepository) : base(dbContext)
        {
            _imageUrlRepository = imageUrlRepository;
        }
        public async Task<List<SportsCourtAppointments>?> GetAllSportsCourtAppointmentsByIdCourt(int id)
        {
            var resp = await (from sca in _dbContext
                .SportsCourtAppointments
                .Include(x => x.SportsCourt)
                .Include(x => x.Client)

                              where sca.IdSportsCourt == id
                              select new SportsCourtAppointments
                              {
                                  InitialDate = sca.InitialDate,
                                  FinalDate = sca.FinalDate,
                                  IdClient = sca.IdClient,
                                  Client = sca.Client,
                                  IdSportsCourt = sca.IdSportsCourt,
                                  SportsCourt = sca.SportsCourt,
                                  Id = sca.Id,
                                  CreatedDate = sca.CreatedDate,
                                  UpdatedDate = sca.UpdatedDate,
                                  Payment = sca.Payment,
                                  Status = sca.Status,
                              })
                         .AsNoTracking()
                     .ToListAsync();
            return resp;
        }
        public async Task<PagedResult<SportsCourtAppointments>> GetAllSportsCourtAppointments(ScheduleFilter scheduleFilter)
        {
            var query = from sca in _dbContext
                .SportsCourtAppointments
                .Include(x => x.SportsCourt)
                .ThenInclude(x => x.SportsCourtCategories)
                            .Include(x => x.SportsCourt).ThenInclude(x => x.sportsCourtImages)
                .Include(x => x.SportsCourt)
                .ThenInclude(x => x.SportsCenter).ThenInclude(x=>x.Address)
                .Include(x => x.Client)
                .Include(x=>x.SportsCategory)
                .Include(x=>x.SportsRegistrations)
                        select sca;
            
            // Aplicar filtros condicionais
            if (scheduleFilter.OpenPublic)
            {
                query = query.Where(sca => sca.OpenPublic == scheduleFilter.OpenPublic);
            }

            if (scheduleFilter.Number!=null && scheduleFilter.Number > 0)
            {
                query = query.Where(sca => sca.Number == scheduleFilter.Number);
            }
            if (scheduleFilter.InitialDate != default)
                query = query.Where(sca => sca.InitialDate.Date >= scheduleFilter.InitialDate);

            if (scheduleFilter.FinalDate != default)
                query = query.Where(sca => sca.FinalDate.Date <= scheduleFilter.FinalDate);

            if (scheduleFilter.IdSportsCourt > 0)
                query = query.Where(sca => sca.IdSportsCourt == scheduleFilter.IdSportsCourt);

            if (scheduleFilter.Status.HasValue)
                query = query.Where(sca => sca.Status == scheduleFilter.Status.Value);

            if (scheduleFilter.IdClient.HasValue && scheduleFilter.IdClient > 0)
                query = query.Where(sca => sca.IdClient == scheduleFilter.IdClient.Value);

            // Filtro para SportsCourtCategories (relacionamento indireto)
            if (scheduleFilter.IdCategory.HasValue && scheduleFilter.IdCategory > 0)
                query = query.Where(sca => sca.IdCategory== scheduleFilter.IdCategory.Value);

            // Filtro para SportsCenter (relacionamento indireto)
            if (scheduleFilter.IdSportsCenter.HasValue && scheduleFilter.IdSportsCenter > 0)
                query = query.Where(sca => sca.SportsCourt.SportsCenter.Id == scheduleFilter.IdSportsCenter.Value);

            PagedResult<SportsCourtAppointments> resp = await query
                .Select(sca => new SportsCourtAppointments
                {
                    Id = sca.Id,
                    InitialDate = sca.InitialDate,
                    FinalDate = sca.FinalDate,
                    IdClient = sca.IdClient,
                    Client = sca.Client,
                    IdSportsCourt = sca.IdSportsCourt,
                    CreatedDate = sca.CreatedDate,
                    UpdatedDate = sca.UpdatedDate,
                    Payment = sca.Payment,
                    Status = sca.Status,
                    Number = sca.Number,
                    Value = sca.Value,
                    PaymentMethod = sca.PaymentMethod,
                    OpenPublic = sca.OpenPublic,
                    Price = sca.Price,
                    SportsCourt = new SportsCourt
                    {
                        Id = sca.SportsCourt.Id,
                        Description = sca.SportsCourt.Description,
                        Name = sca.SportsCourt.Name,
                        Price=sca.SportsCourt.Price,
                        SportsCenter = sca.SportsCourt.SportsCenter,
                        sportsCourtImages= sca.SportsCourt.sportsCourtImages.Select(x=> new SportsCourtImage
                        {
                            UniqueFileName=x.UniqueFileName,
                            ImageUrl=_imageUrlRepository.GetImageUrl(x.UniqueFileName)
                        }).ToList(),
                    },
                    SportsCategory=sca.SportsCategory,
                    SportsRegistrations=sca.SportsRegistrations
                })
                .OrderByDescending(x=>x.Id)
                .AsNoTracking()
                .PaginateAsync(new PaginationParameters { Page = scheduleFilter.Page, PageSize = scheduleFilter.PageSize });

            return resp;
        }
        public async Task<SportsCourtAppointments?> GetLinksById(int sportsCourtAppointmentsId)
        {
            var query = await (from sca in _dbContext
               .SportsCourtAppointments
               .Include(x => x.SportsCourt)
               .ThenInclude(x => x.SportsCourtCategories)
                           .Include(x => x.SportsCourt).ThenInclude(x => x.sportsCourtImages)
               .Include(x => x.SportsCourt)
               .ThenInclude(x => x.SportsCenter).ThenInclude(x => x.Address)
               .Include(x => x.Client)
               .Include(x => x.SportsCategory)
               .Include(x => x.SportsRegistrations)
               where sca.Id==sportsCourtAppointmentsId
                        select  new SportsCourtAppointments
                {
                    Id = sca.Id,
                    InitialDate = sca.InitialDate,
                    FinalDate = sca.FinalDate,
                    IdClient = sca.IdClient,
                    Client = sca.Client,
                    IdSportsCourt = sca.IdSportsCourt,
                    CreatedDate = sca.CreatedDate,
                    UpdatedDate = sca.UpdatedDate,
                    Payment = sca.Payment,
                    Status = sca.Status,
                    Number = sca.Number,
                    Value = sca.Value,
                    PaymentMethod = sca.PaymentMethod,
                    OpenPublic = sca.OpenPublic,
                    Price = sca.Price,
                    SportsCourt = new SportsCourt
                    {
                        Id = sca.SportsCourt.Id,
                        Description = sca.SportsCourt.Description,
                        Name = sca.SportsCourt.Name,
                        Price = sca.SportsCourt.Price,
                        SportsCenter = sca.SportsCourt.SportsCenter,
                        sportsCourtImages = sca.SportsCourt.sportsCourtImages.Select(x => new SportsCourtImage
                        {
                            UniqueFileName = x.UniqueFileName,
                            ImageUrl = _imageUrlRepository.GetImageUrl(x.UniqueFileName)
                        }).ToList(),
                    },
                    SportsCategory = sca.SportsCategory,
                    SportsRegistrations = sca.SportsRegistrations
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return query;
        }

    }
    public interface ISportsCourtAppointmentsRepository : IGenericRepository<SportsCourtAppointments>
    {
        Task<PagedResult<SportsCourtAppointments>> GetAllSportsCourtAppointments(ScheduleFilter scheduleFilter);
        Task<List<SportsCourtAppointments>?> GetAllSportsCourtAppointmentsByIdCourt(int id);
        Task<SportsCourtAppointments?> GetLinksById(int sportsCourtAppointmentsId);
    }
}
