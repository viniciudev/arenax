using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;

namespace Services
{


    public class SportsCourtOperationService : ISportsCourtOperationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        public SportsCourtOperationService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }

        public async Task CreateSportsCourtOperation(SportsCourtOperationDto sportsCourtOperationDto)
        {
            SportsCourtOperation sportsCourtOperation = _mapper.Map<SportsCourtOperation>(sportsCourtOperationDto);
            await _unitOfWork.SportsCourtOperation.Add(sportsCourtOperation);
            _unitOfWork.Save();
        }

        public async Task<bool> DeleteSportsCourtOperation(int SportsCourtOperationId)
        {
            if (SportsCourtOperationId > 0)
            {
                var SportsCourtOperationDetail = await _unitOfWork.SportsCourtOperation.GetById(SportsCourtOperationId);
                if (SportsCourtOperationDetail != null)
                {
                    _unitOfWork.SportsCourtOperation.Delete(SportsCourtOperationDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<SportsCourtOperation?> GetSportsCourtOperationById(int SportsCourtOperationId)
        {
            if (SportsCourtOperationId > 0)
            {
                return await _unitOfWork.SportsCourtOperation.GetById(SportsCourtOperationId);
            }
            return null;
        }

        public async Task<bool> UpdateSportsCourtOperation(SportsCourtOperationDto SportsCourtOperationParam)
        {
            if (SportsCourtOperationParam != null)
            {
                var SportsCourtOperation = await _unitOfWork.SportsCourtOperation.GetById(SportsCourtOperationParam.Id??0);
                if (SportsCourtOperation != null)
                {
                    SportsCourtOperation.DayOfWeek = SportsCourtOperationParam.DayOfWeek;
                    SportsCourtOperation.Start=SportsCourtOperationParam.Start;
                    SportsCourtOperation.End=SportsCourtOperationParam.End;
                    _unitOfWork.SportsCourtOperation.Update(SportsCourtOperation);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }
        public async Task<List<SportsCourtOperation>?> GetAllSportsCourtOperationByIdCourt(int id)
        {
            if (id > 0)
            {
                return await _unitOfWork.SportsCourtOperation.GetAllSportsCourtOperationByIdCourt(id);
            }
            return null;
        }
    }

    public interface ISportsCourtOperationService
    {
        Task CreateSportsCourtOperation(SportsCourtOperationDto SportsCourtOperation);
        Task<SportsCourtOperation?> GetSportsCourtOperationById(int SportsCourtOperationId);
        Task<bool> UpdateSportsCourtOperation(SportsCourtOperationDto SportsCourtOperationParam);
        Task<bool> DeleteSportsCourtOperation(int SportsCourtOperationId);
        Task<List<SportsCourtOperation>?> GetAllSportsCourtOperationByIdCourt(int id);
    }


}
