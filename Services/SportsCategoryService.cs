using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;

namespace Services
{

    public class SportsCategoryService : ISportsCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        public SportsCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }

        public async Task CreateSportsCategory(SportsCategoryDto sportsCategoryDto)
        {
            SportsCategory sportsCategory = _mapper.Map<SportsCategory>(sportsCategoryDto);
            await _unitOfWork.SportsCategory.Add(sportsCategory);
            _unitOfWork.Save();
        }

        public async Task<bool> DeleteSportsCategory(int SportsCategoryId)
        {
            if (SportsCategoryId > 0)
            {
                var SportsCategoryDetail = await _unitOfWork.SportsCategory.GetById(SportsCategoryId);
                if (SportsCategoryDetail != null)
                {
                    _unitOfWork.SportsCategory.Delete(SportsCategoryDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<SportsCategory?> GetSportsCategoryById(int SportsCategoryId)
        {
            if (SportsCategoryId > 0)
            {
                return await _unitOfWork.SportsCategory.GetById(SportsCategoryId);
            }
            return null;
        }

        public async Task<bool> UpdateSportsCategory(SportsCategoryDto SportsCategoryParam)
        {
            if (SportsCategoryParam != null)
            {
                var SportsCategory = await _unitOfWork.SportsCategory.GetById(SportsCategoryParam.Id);
                if (SportsCategory != null)
                {
                    SportsCategory.Description = SportsCategoryParam.Description;
                    _unitOfWork.SportsCategory.Update(SportsCategory);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }
        public async Task<List<SportsCategory>?> GetAllSportsCategory()
        {

            return await _unitOfWork.SportsCategory.GetAll();

        }
    }

    public interface ISportsCategoryService
    {
        Task CreateSportsCategory(SportsCategoryDto SportsCategory);
        Task<SportsCategory?> GetSportsCategoryById(int SportsCategoryId);
        Task<bool> UpdateSportsCategory(SportsCategoryDto SportsCategoryParam);
        Task<bool> DeleteSportsCategory(int SportsCategoryId);
        Task<List<SportsCategory>?> GetAllSportsCategory();
    }
}
