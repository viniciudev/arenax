using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;

namespace Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        public NotificationsService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }

        public async Task<int> CreateNotifications(NotificationsDto modelDto)
        {
            Notifications model = _mapper.Map<Notifications>(modelDto);


            await _unitOfWork.Notifications.Add(model);
            _unitOfWork.Save();
            return model.Id;

        }

        public async Task<bool> DeleteNotifications(int NotificationsId)
        {
            if (NotificationsId > 0)
            {
                var NotificationsDetail = await _unitOfWork.Notifications.GetById(NotificationsId);
                if (NotificationsDetail != null)
                {
                    _unitOfWork.Notifications.Delete(NotificationsDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<Notifications?> GetNotificationsById(int NotificationsId)
        {
            if (NotificationsId > 0)
            {
                var resp = await _unitOfWork.Notifications.GetById(NotificationsId);
                return resp;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateNotifications(NotificationsDto dto)
        {
            if (dto != null)
            {
                var Notifications = await _unitOfWork.Notifications.GetById(dto.Id);
                if (Notifications != null)
                {
                    Notifications.Title =!string.IsNullOrEmpty(dto.Title)?dto.Title: Notifications.Title;
                    Notifications.Body = !string.IsNullOrEmpty(dto.Body)?dto.Body: Notifications.Body;
                    Notifications.Read= dto.Read;
                    Notifications.IdClient= dto.IdClient>0?dto.IdClient:Notifications.IdClient;

                    _unitOfWork.Notifications.Update(Notifications);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }
        public async Task<List<Notifications>> GetAllNotificationsByIdClient(int id)
        {
            return await _unitOfWork.Notifications.GetAllByIdClient(id);
        }

    }

    public interface INotificationsService
    {
        Task<int> CreateNotifications(NotificationsDto Notifications);
        Task<Notifications?> GetNotificationsById(int NotificationsId);
        Task<bool> UpdateNotifications(NotificationsDto NotificationsParam);
        Task<bool> DeleteNotifications(int NotificationsId);
        Task<List<Notifications>> GetAllNotificationsByIdClient(int id);
    }
}
