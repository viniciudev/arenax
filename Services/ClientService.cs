using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;

namespace Services
{

    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }

        public async Task<int> CreateClients(ClientDto Dto)
        {
            Client model = _mapper.Map<Client>(Dto);
            await _unitOfWork.Client.Add(model);
            _unitOfWork.Save();
            return model.Id;
        }

        public async Task<bool> DeleteClients(int ClientsId)
        {
            if (ClientsId > 0)
            {
                var ClientsDetail = await _unitOfWork.Client.GetById(ClientsId);
                if (ClientsDetail != null)
                {
                    _unitOfWork.Client.Delete(ClientsDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<Client?> GetClientsById(int ClientsId)
        {
            if (ClientsId > 0)
            {
                return await _unitOfWork.Client.GetById(ClientsId);
            }
            return null;
        }

        public async Task<bool> UpdateClients(ClientDto ClientsParam)
        {
            if (ClientsParam != null)
            {
                var Clients = await _unitOfWork.Client.GetById(ClientsParam.Id);
                if (Clients != null)
                {

                    Clients.Name = ClientsParam.Name;
                    Clients.Cellphone = ClientsParam.Cellphone;
                    Clients.Email = ClientsParam.Email;
                    Clients.TokenApp = ClientsParam.TokenApp;
                    _unitOfWork.Client.Update(Clients);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }
        public async Task<List<Client>?> GetAllClients()
        {
           
                return await _unitOfWork.Client.GetAllClients();
            
        }
        public async Task<ResponseClient?> GetClientsByPhone(string phone)
        {
            return await _unitOfWork.Client.GetClientsByPhone(phone);
        }
        public async Task<bool> PostTokenApp(TokenAppRequest value)
        {
            Client client =await _unitOfWork.Client.GetById(value.IdClient);
            if (client != null)
            {
                client.TokenApp = value.Token;
                _unitOfWork.Client.Update(client);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
    }

    public interface IClientService
    {
        Task<int> CreateClients(ClientDto Clients);
        Task<Client?> GetClientsById(int ClientsId);
        Task<bool> UpdateClients(ClientDto ClientsParam);
        Task<bool> DeleteClients(int ClientsId);
        
        Task<ResponseClient?> GetClientsByPhone(string phone);
        Task<List<Client>?> GetAllClients();
        Task<bool> PostTokenApp(TokenAppRequest value);
    }
}
