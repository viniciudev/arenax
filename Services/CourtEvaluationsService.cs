using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  
    public class CourtEvaluationsService : ICourtEvaluationsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        public CourtEvaluationsService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }

        public async Task CreateCourtEvaluations(CourtEvaluationsDto courtEvaluationsDto)
        {
            CourtEvaluations courtEvaluations = _mapper.Map<CourtEvaluations>(courtEvaluationsDto);
            await _unitOfWork.CourtEvaluations.Add(courtEvaluations);
            _unitOfWork.Save();
        }

        public async Task<bool> DeleteCourtEvaluations(int CourtEvaluationsId)
        {
            if (CourtEvaluationsId > 0)
            {
                var CourtEvaluationsDetail = await _unitOfWork.CourtEvaluations.GetById(CourtEvaluationsId);
                if (CourtEvaluationsDetail != null)
                {
                    _unitOfWork.CourtEvaluations.Delete(CourtEvaluationsDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<CourtEvaluations?> GetCourtEvaluationsById(int CourtEvaluationsId)
        {
            if (CourtEvaluationsId > 0)
            {
                return await _unitOfWork.CourtEvaluations.GetById(CourtEvaluationsId);
            }
            return null;
        }

        public async Task<bool> UpdateCourtEvaluations(CourtEvaluationsDto CourtEvaluationsParam)
        {
            if (CourtEvaluationsParam != null)
            {
                var CourtEvaluations = await _unitOfWork.CourtEvaluations.GetById(CourtEvaluationsParam.Id);
                if (CourtEvaluations != null)
                {
                    CourtEvaluations.Description = CourtEvaluationsParam.Description;
                    CourtEvaluations.Note = CourtEvaluationsParam.Note;
                    _unitOfWork.CourtEvaluations.Update(CourtEvaluations);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }
        public async Task<List<CourtEvaluations>?> GetAllCourtEvaluationsByIdCourt(int id)
        {
            return await _unitOfWork.CourtEvaluations.GetAllCourtEvaluationsByIdCourt(id);
        }
    }

    public interface ICourtEvaluationsService
    {
        Task CreateCourtEvaluations(CourtEvaluationsDto CourtEvaluations);
        Task<CourtEvaluations?> GetCourtEvaluationsById(int CourtEvaluationsId);
        Task<bool> UpdateCourtEvaluations(CourtEvaluationsDto CourtEvaluationsParam);
        Task<bool> DeleteCourtEvaluations(int CourtEvaluationsId);
        Task<List<CourtEvaluations>?> GetAllCourtEvaluationsByIdCourt(int id);
    }
}
