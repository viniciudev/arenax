using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;

namespace Services
{
    public class SportsCenterUsersService : ISportsCenterUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SportsCenterUsersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SportsCenterUsersDto?> GetSportsCenterUsersByIdUser(int idUser)
        {
            if (idUser > 0)
            {
                return await _unitOfWork.SportsCenterUsers.GetByIdUser(idUser);
            }
            return null;
        }
    }

    public interface ISportsCenterUsersService
    {

        Task<SportsCenterUsersDto?> GetSportsCenterUsersByIdUser(int idUser);

    }
}
