using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Migrations;
using Infrastructure.Repositories;

namespace Services
{

    public class SportsRegistrationService : ISportsRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        private readonly IFirebaseNotificationService _firebaseNotificationService;
        private readonly ISportsCourtAppointmentsService _sportsCourtAppointmentsService;
        public SportsRegistrationService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager,
            IFirebaseNotificationService firebaseNotificationService, ISportsCourtAppointmentsService sportsCourtAppointmentsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
            _firebaseNotificationService = firebaseNotificationService;
            _sportsCourtAppointmentsService = sportsCourtAppointmentsService;
        }

        public async Task<int> CreateSportsRegistrations(SportsRegistrationDto Dto)
        {
            SportsRegistration model = _mapper.Map<SportsRegistration>(Dto);
            await _unitOfWork.SportsRegistration.Add(model);
            _unitOfWork.Save();
            SportsCourtAppointments ?sportsCourtAppointments = await _sportsCourtAppointmentsService.GetSportsCourtAppointmentsById(Dto.IdSportsCourtAppointments);
            if (sportsCourtAppointments!=null && sportsCourtAppointments.Client .Id > 0)
            {
                Client client = await _unitOfWork.Client.GetById(sportsCourtAppointments.Client.Id);
                if (client != null && !string.IsNullOrEmpty(client.TokenApp))
                {

                    Client clientSolicity = await _unitOfWork.Client.GetById(Dto.IdClient);
                    DateTime dateTime = sportsCourtAppointments.InitialDate;
                        await _firebaseNotificationService.SendNotificationAsync(
                             new NotificationRequest
                             {
                                 IdClient = Dto.IdClient,
                                 DeviceToken = client.TokenApp,
                                 Notification = new FirebaseNotification
                                 {
                                     Body = $"Solicitação para entrar no horário {dateTime:dd/MM/yy} às {dateTime:HH}h{dateTime:mm}",
                                     Title = $"{clientSolicity.Name} quer particiar do seu esporte!"
                                 }
                             }
                             );
                    
                }
            }
            return model.Id;
        }

        public async Task<bool> DeleteSportsRegistrations(int SportsRegistrationsId)
        {
            if (SportsRegistrationsId > 0)
            {
                var SportsRegistrationsDetail = await _unitOfWork.SportsRegistration.GetById(SportsRegistrationsId);
                if (SportsRegistrationsDetail != null)
                {
                    _unitOfWork.SportsRegistration.Delete(SportsRegistrationsDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<SportsRegistration?> GetSportsRegistrationsById(int SportsRegistrationsId)
        {
            if (SportsRegistrationsId > 0)
            {
                return await _unitOfWork.SportsRegistration.GetById(SportsRegistrationsId);
            }
            return null;
        }

        public async Task<bool> UpdateSportsRegistrations(SportsRegistrationDto SportsRegistrationsParam)
        {
            if (SportsRegistrationsParam != null)
            {
                var SportsRegistrations = await _unitOfWork.SportsRegistration.GetById(SportsRegistrationsParam.Id);
                if (SportsRegistrations != null)
                {

                    SportsRegistrations.Status = SportsRegistrationsParam.Status;

                    _unitOfWork.SportsRegistration.Update(SportsRegistrations);
                    var result = _unitOfWork.Save();
                    if (SportsRegistrationsParam.IdClient > 0)
                    {
                        Client client = await _unitOfWork.Client.GetById((int)SportsRegistrationsParam.IdClient);
                        if (client != null && !string.IsNullOrEmpty(client.TokenApp))
                        {
                            if (SportsRegistrations.Status != StatusSportsRegistration.pending)
                            {
                                SportsCourtAppointments sportsCourtAppointments = await _unitOfWork.SportsCourtAppointments.GetById(SportsRegistrationsParam.IdSportsCourtAppointments);
                                DateTime dateTime = sportsCourtAppointments.InitialDate;
                                await _firebaseNotificationService.SendNotificationAsync(
                                     new NotificationRequest
                                     {
                                         IdClient = SportsRegistrationsParam.IdClient,
                                         DeviceToken = client.TokenApp,
                                         Notification = new FirebaseNotification
                                         {
                                             Body = SportsRegistrationsParam.Status == StatusSportsRegistration.approved ?
                                             $"Sua solicitação ao horário de {sportsCourtAppointments?.SportsCategory?.Description ?? "Esporte"} foi aprovado!"
                                             : $"Sua solicitação ao horário de {sportsCourtAppointments?.SportsCategory?.Description ?? "Esporte"} não foi aprovado!",
                                             Title = $"Solicitação {dateTime:dd/MM/yy} às {dateTime:HH}h{dateTime:mm}"
                                         }
                                     }
                                     );
                            }
                        }
                    }
                    if (SportsRegistrationsParam.Status == StatusSportsRegistration.approved)
                    {
                        //notificação de aprovado
                    }
                    return result > 0;
                }
            }
            return false;
        }
        public async Task<List<SportsRegistration>?> GetAllSportsRegistrations(int id)
        {

            return await _unitOfWork.SportsRegistration.GetAllSportsRegistrations(id);

        }
        public async Task<List<SportsRegistration>?> GetAllByIdClient(int id)
        {
            return await _unitOfWork.SportsRegistration.GetAllByIdClient(id);
        }
    }

    public interface ISportsRegistrationService
    {
        Task<int> CreateSportsRegistrations(SportsRegistrationDto SportsRegistrations);
        Task<SportsRegistration?> GetSportsRegistrationsById(int SportsRegistrationsId);
        Task<bool> UpdateSportsRegistrations(SportsRegistrationDto SportsRegistrationsParam);
        Task<bool> DeleteSportsRegistrations(int SportsRegistrationsId);
        Task<List<SportsRegistration>?> GetAllSportsRegistrations(int id);
        Task<List<SportsRegistration>?> GetAllByIdClient(int id);
    }
}
