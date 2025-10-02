using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;

namespace Services
{

    public class SportsCourtService : ISportsCourtService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;

        public SportsCourtService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }

        public async Task<int> CreateSportsCourt(SportCourtDto sportCourtDto)
        {
            SportsCourt sportCourt = _mapper.Map<SportsCourt>(sportCourtDto);
            await _unitOfWork.SportsCourt.Add(sportCourt);
            _unitOfWork.Save();
            foreach (var id in sportCourtDto.CategoryIds)
            {
                if (id != 0)
                    await _unitOfWork.SportsCourtCategory.Add(new SportsCourtCategory
                    {
                        SportsCategoryId = id,
                        SportsCourtId = sportCourt.Id
                    });
            }
            _unitOfWork.Save();
            return sportCourt.Id;
          

        }

        public async Task<bool> DeleteSportsCourt(int SportsCourtId)
        {
            if (SportsCourtId > 0)
            {
                var SportsCourtDetail = await _unitOfWork.SportsCourt.GetById(SportsCourtId);
                if (SportsCourtDetail != null)
                {
                    _unitOfWork.SportsCourt.Delete(SportsCourtDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<SportsCourt?> GetSportsCourtById(int SportsCourtId)
        {
            if (SportsCourtId > 0)
            {
                var resp = await _unitOfWork.SportsCourt.GetByIdAsync(SportsCourtId);
                return resp;
            }
            else
            {
                return null;
            }

        }
        public async Task UpdateSportsCourtImage(int id, List<string> savedFiles)
        {
            var sportsCourt = await _unitOfWork.SportsCourt.GetById(id);
            if (sportsCourt == null)
            {
                throw new ArgumentException("Quadra não encontrado");
            }
            // Atualize o logo no objeto
            foreach (var item in savedFiles)
            {
               await _unitOfWork.SportsCourtImage.Add(new SportsCourtImage
                {
                    UniqueFileName=item,
                    SportsCourtId=id
                });
            }
            _unitOfWork.Save();
        }

        public async Task<bool> UpdateSportsCourt(SportCourtDto SportsCourtParam)
        {
            if (SportsCourtParam != null)
            {
                SportsCourt? sportsCourt = await _unitOfWork.SportsCourt.GetByIdAsync(SportsCourtParam.Id);
                if (sportsCourt != null)
                {
                    sportsCourt.Name = SportsCourtParam.Name;
                    sportsCourt.SubName = SportsCourtParam.SubName;
                    sportsCourt.Description = SportsCourtParam.Description;
                    sportsCourt.Price = SportsCourtParam.Price;
                    _unitOfWork.SportsCourt.Update(sportsCourt);

                    if (SportsCourtParam.CategoryIds.Count > 0)
                    {

                        //deletar para recriar
                        foreach (var item in sportsCourt.SportsCourtCategories)
                        {
                            _unitOfWork.SportsCourtCategory.Delete(item);
                            //_unitOfWork.Save();
                        }

                        //recriar
                        foreach (var id in SportsCourtParam.CategoryIds)
                        {
                            if (id != 0)
                                await _unitOfWork.SportsCourtCategory.Add(new SportsCourtCategory
                                {
                                    SportsCategoryId = id,
                                    SportsCourtId = sportsCourt.Id
                                });
                        }
                    }
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }
        public async Task<List<SportsCourt>?> getall(int tenantID)
        {
            return await _unitOfWork.SportsCourt.GetAllByIdSportsCenter(tenantID);
        }
        public async Task DeleteSportsCourtImage( string filename)
        {
           SportsCourtImage? sportsCourtImage= 
                await _unitOfWork.SportsCourtImage.GetByName( filename);

            if (sportsCourtImage != null)
            {
                _unitOfWork.SportsCourtImage.Delete(sportsCourtImage);
                _unitOfWork.Save();
            }

        }
    }

    public interface ISportsCourtService
    {
        Task<int> CreateSportsCourt(SportCourtDto SportsCourt);
        Task<SportsCourt?> GetSportsCourtById(int SportsCourtId);
        Task<bool> UpdateSportsCourt(SportCourtDto SportsCourtParam);
        Task<bool> DeleteSportsCourt(int SportsCourtId);
        Task UpdateSportsCourtImage(int id, List<string> savedFiles);
        Task<List<SportsCourt>?> getall(int tenantID);
        Task DeleteSportsCourtImage( string filename);
    }
}
