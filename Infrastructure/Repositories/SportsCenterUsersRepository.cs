using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{


     public class SportsCenterUsersRepository : GenericRepository<SportsCenterUsers>, ISportsCenterUsersRepository
    {
        private readonly IMapper _mapper;
        public SportsCenterUsersRepository(DbContextClass dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
        //public async Task<List<SportsCenterUsers>?> GetAllSportsCenterUsersByIdCourt(int id)
        //{
        //    var resp = await _dbContext.SportsCenterUsers.Where(x => x.IdSportsCourt == id).AsNoTracking().ToListAsync();
        //    return resp;
        //}
        public async Task<SportsCenterUsersDto?> GetByIdUser(int id)
        {
            var resp = await ( from scu in _dbContext. SportsCenterUsers

                where(scu.IdUser == id)
                select new SportsCenterUsersDto
                {
                    IdUser = scu.IdUser,
                    SportsCenter = new SportsCenterDto
                    {
                        Id = scu.SportsCenter.Id,
                        Name = scu.SportsCenter.Name,
                        Cnpj = scu.SportsCenter.Cnpj,
                        Phone = scu.SportsCenter.Phone,
                        Address = scu.SportsCenter.Address != null ? new AddressDto
                        {
                            Cep = scu.SportsCenter.Address.Cep,
                            StreetAddress = scu.SportsCenter.Address.StreetAddress,
                            Complement = scu.SportsCenter.Address.Complement,
                            District = scu.SportsCenter.Address.District,
                            City = scu.SportsCenter.Address.City,
                            Uf = scu.SportsCenter.Address.Uf,
                            State = scu.SportsCenter.Address.State
                        } : null,
                        OpeningHours = scu.SportsCenter.OpeningHours != null ? new OpeningHoursDto
                        {
                            Monday = scu.SportsCenter.OpeningHours.Monday,
                            Tuesday = scu.SportsCenter.OpeningHours.Tuesday,
                            Wednesday = scu.SportsCenter.OpeningHours.Wednesday,
                            Thursday = scu.SportsCenter.OpeningHours.Thursday,
                            Friday = scu.SportsCenter.OpeningHours.Friday,
                            Saturday = scu.SportsCenter.OpeningHours.Saturday,
                            Sunday = scu.SportsCenter.OpeningHours.Sunday
                        } : null,
                        IdUser = scu.IdUser
                    },
                    User= new UserDto
                    {
                        Email=scu.User.Email,
                        Name=scu.User.Name,
                        Password=scu.User.Password,
                        Id = scu.User.Id,
                    }
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return resp;
        }
    }
    public interface ISportsCenterUsersRepository : IGenericRepository<SportsCenterUsers>
    {
        //Task<List<SportsCenterUsers>?> GetAllSportsCenterUsersByIdCourt(int id);
        Task<SportsCenterUsersDto?> GetByIdUser(int sportsCenterUsersId);
    }
}
