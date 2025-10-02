using Core.DTOs;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using Services;

public interface IFirebaseNotificationService
{
    Task<NotificationResponse> SendNotificationAsync(NotificationRequest request);
    Task<NotificationResponse> SendNotificationToTopicAsync(string topic, FirebaseNotification notification);
    Task<NotificationResponse> SendNotificationToMultipleDevicesAsync(List<string> deviceTokens, FirebaseNotification notification);
}

public class FirebaseNotificationService : IFirebaseNotificationService
{
    private readonly ILogger<FirebaseNotificationService> _logger;
    private readonly INotificationsService _notificationsService;
    public FirebaseNotificationService(ILogger<FirebaseNotificationService> logger,INotificationsService notificationsService)
    {
        _logger = logger;
        _notificationsService = notificationsService;
    }

    public async Task<NotificationResponse> SendNotificationAsync(NotificationRequest request)
    {
        try
        {
            var message = new Message()
            {
                Token = request.DeviceToken,
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Title = request.Notification.Title,
                    Body = request.Notification.Body,
                    //ImageUrl = request.Notification.ImageUrl
                },
                //Data = request.Notification.Data,
                Android = new AndroidConfig()
                {
                    Priority = Priority.High,
                    Notification = new AndroidNotification()
                    {
                        ChannelId = "high_importance_channel",
                        Priority = NotificationPriority.MAX
                    }
                },
                Apns = new ApnsConfig()
                {
                    Aps = new Aps()
                    {
                        Alert = new ApsAlert()
                        {
                            Title = request.Notification.Title,
                            Body = request.Notification.Body
                        },
                        Sound = "default",
                        Badge = 1
                    }
                }
            };
            await _notificationsService.CreateNotifications(new NotificationsDto
            {
                Title = request.Notification.Title,
                Body = request.Notification.Body,
                IdClient = request.IdClient,
                Read = false
            });

            var app= FirebaseApp.GetInstance("arenaxjogador");
            var messaging = FirebaseMessaging.GetMessaging(app);
            await messaging.SendAsync(message);
            //_logger.LogInformation("Notificação enviada com sucesso: {ResponseId}", response);

          
            return new NotificationResponse
            {
                Success = true,
                Message = "Notificação enviada com sucesso",
                ResponseId = messaging.ToString(),
            };
        }
        catch (FirebaseMessagingException ex)
        {
            //_logger.LogError(ex, "Erro ao enviar notificação Firebase");
            return new NotificationResponse
            {
                Success = false,
                Message = $"Erro Firebase: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Erro inesperado ao enviar notificação");
            return new NotificationResponse
            {
                Success = false,
                Message = $"Erro inesperado: {ex.Message}"
            };
        }
    }

    public async Task<NotificationResponse> SendNotificationToTopicAsync(string topic, FirebaseNotification notification)
    {
        try
        {
            var message = new Message()
            {
                Topic = topic,
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Title = notification.Title,
                    Body = notification.Body,
                    ImageUrl = notification.ImageUrl
                },
                Data = notification.Data
            };

            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            return new NotificationResponse
            {
                Success = true,
                Message = "Notificação para tópico enviada com sucesso",
                ResponseId = response
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar notificação para tópico");
            return new NotificationResponse
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<NotificationResponse> SendNotificationToMultipleDevicesAsync(
        List<string> deviceTokens, FirebaseNotification notification)
    {
        try
        {
            var message = new MulticastMessage()
            {
                Tokens = deviceTokens,
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Title = notification.Title,
                    Body = notification.Body,
                    ImageUrl = notification.ImageUrl
                },
                Data = notification.Data
            };

            var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);

            return new NotificationResponse
            {
                Success = response.SuccessCount > 0,
                Message = $"Enviado para {response.SuccessCount} de {response.Responses.Count} dispositivos",
                ResponseId = response.Responses.FirstOrDefault()?.MessageId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar notificação múltipla");
            return new NotificationResponse
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}