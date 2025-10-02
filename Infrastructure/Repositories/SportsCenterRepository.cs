using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure.Repositories
{

    public class SportsCenterRepository : GenericRepository<SportsCenter>, ISportsCenterRepository
    {
        public SportsCenterRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        public async Task<SportsCenterDto?> GetByIdAsync(int sportsCenterId)
        {
            var resp = await(from sc in _dbContext.SportsCenter.Include(x => x.OpeningHours).Include(x => x.Address)
                             where sc.Id == sportsCenterId
                select new SportsCenterDto
                {
                    Id = sc.Id,
                    IdUser = 0,
                    Name = sc.Name,
                    UniqueFileName = sc.UniqueFileName,
                    Address = new AddressDto
                    {
                        Cep = sc.Address.Cep,
                        StreetAddress = sc.Address.StreetAddress,
                        Complement = sc.Address.Complement,
                        District = sc.Address.District,
                        City = sc.Address.City,
                        Uf = sc.Address.Uf,
                        State = sc.Address.State
                    },
                    Phone = sc.Phone,
                    OpeningHours = new OpeningHoursDto
                    {
                        Monday = sc.OpeningHours.Monday,
                        Sunday = sc.OpeningHours.Sunday,
                        Thursday = sc.OpeningHours.Thursday,
                        Tuesday = sc.OpeningHours.Tuesday,
                        Friday = sc.OpeningHours.Friday,
                        Saturday = sc.OpeningHours.Saturday,
                        Wednesday = sc.OpeningHours.Wednesday
                    },
                    Amenities = JsonExtensions.DeserializeStringListOrEmpty(sc.Amenities),
                    Cnpj = sc.Cnpj
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return resp;
        }
        public async Task<List<SportsCenterDto>> GetAllByFilter(SportCenterFilter value)
        {
            var resp = await _dbContext.SportsCenter
     .Include(x => x.OpeningHours)
     .Include(x => x.Address)
     .Where(sc => (string.IsNullOrEmpty(value.Name) || sc.Name.Contains(value.Name))
         && (string.IsNullOrEmpty(value.Cep) || sc.Address.Cep.Contains(value.Cep))
         && (string.IsNullOrEmpty(value.StreetAddress) || sc.Address.StreetAddress.Contains(value.StreetAddress))
         && (string.IsNullOrEmpty(value.Complement) || sc.Address.Complement.Contains(value.Complement))
         && (string.IsNullOrEmpty(value.District) || sc.Address.District.Contains(value.District))
         && (string.IsNullOrEmpty(value.City) || sc.Address.City.Contains(value.City))
         && (string.IsNullOrEmpty(value.Uf) || sc.Address.Uf.Contains(value.Uf))
         && (string.IsNullOrEmpty(value.State) || sc.Address.State.Contains(value.State)))
     .Select(sc => new SportsCenterDto
     {
         Id = sc.Id,
         IdUser = 0,
         Name = sc.Name,
         UniqueFileName = sc.UniqueFileName,
         Address = new AddressDto
         {
             Cep = sc.Address.Cep,
             StreetAddress = sc.Address.StreetAddress,
             Complement = sc.Address.Complement,
             District = sc.Address.District,
             City = sc.Address.City,
             Uf = sc.Address.Uf,
             State = sc.Address.State
         },
         Phone = sc.Phone,
         OpeningHours = new OpeningHoursDto
         {
             Monday = sc.OpeningHours.Monday,
             Sunday = sc.OpeningHours.Sunday,
             Thursday = sc.OpeningHours.Thursday,
             Tuesday = sc.OpeningHours.Tuesday,
             Friday = sc.OpeningHours.Friday,
             Saturday = sc.OpeningHours.Saturday,
             Wednesday = sc.OpeningHours.Wednesday
         },
         Amenities = JsonExtensions.DeserializeStringListOrEmpty(sc.Amenities),
         Cnpj =sc.Cnpj
     })
     .ToListAsync();
            return resp;
        }
        public async Task UpdatePartialAsync(int id, Action<SportsCenter> updateAction)
        {
            var entity = await _dbContext.SportsCenter
                .Include(sc => sc.Address)
                .Include(sc => sc.OpeningHours)
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (entity != null)
            {
                updateAction(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }
        public static class JsonExtensions
        {
            public static List<string> DeserializeStringListOrEmpty(string json)
            {
                if (string.IsNullOrWhiteSpace(json))
                    return new List<string>();

                try
                {
                    return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
                }
                catch (JsonException)
                {
                    return new List<string>();
                }
            }
        }

    }
    public interface ISportsCenterRepository : IGenericRepository<SportsCenter>
    {
        Task<List<SportsCenterDto>> GetAllByFilter(SportCenterFilter value);

        Task<SportsCenterDto?> GetByIdAsync(int sportsCenterId);
        Task UpdatePartialAsync(int id, Action<SportsCenter> updateAction);
    }
}
