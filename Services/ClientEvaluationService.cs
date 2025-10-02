using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;

namespace Services
{

    public class ClientEvaluationService : IClientEvaluationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        public ClientEvaluationService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }

        public async Task<int> CreateClientEvaluations(ClientEvaluationDto Dto)
        {
            ClientEvaluation model = _mapper.Map<ClientEvaluation>(Dto);
            await _unitOfWork.ClientEvaluation.Add(model);
            _unitOfWork.Save();
            return model.Id;
        }

        public async Task<bool> DeleteClientEvaluations(int ClientEvaluationsId)
        {
            if (ClientEvaluationsId > 0)
            {
                var ClientEvaluationsDetail = await _unitOfWork.ClientEvaluation.GetById(ClientEvaluationsId);
                if (ClientEvaluationsDetail != null)
                {
                    _unitOfWork.ClientEvaluation.Delete(ClientEvaluationsDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<ClientEvaluation?> GetClientEvaluationsById(int ClientEvaluationsId)
        {
            if (ClientEvaluationsId > 0)
            {
                return await _unitOfWork.ClientEvaluation.GetById(ClientEvaluationsId);
            }
            return null;
        }

        public async Task<bool> UpdateClientEvaluations(ClientEvaluationDto ClientEvaluationsParam)
        {
            if (ClientEvaluationsParam != null)
            {
                var ClientEvaluations = await _unitOfWork.ClientEvaluation.GetById(ClientEvaluationsParam.Id);
                if (ClientEvaluations != null)
                {

                    ClientEvaluations.Description = ClientEvaluationsParam.Description;
                    ClientEvaluations.Note = ClientEvaluationsParam.Note;

                    _unitOfWork.ClientEvaluation.Update(ClientEvaluations);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }
     
        public async Task<PlayerMediaResponse> GetAllClientEvaluationsByIdClient(int id)
        {
            return await _unitOfWork.ClientEvaluation.GetAllByIdClient(id);
        }
    }

    public interface IClientEvaluationService
    {
        Task<int> CreateClientEvaluations(ClientEvaluationDto ClientEvaluations);
        Task<ClientEvaluation?> GetClientEvaluationsById(int ClientEvaluationsId);
        Task<bool> UpdateClientEvaluations(ClientEvaluationDto ClientEvaluationsParam);
        Task<bool> DeleteClientEvaluations(int ClientEvaluationsId);
        Task<PlayerMediaResponse> GetAllClientEvaluationsByIdClient(int id);
    }
}
