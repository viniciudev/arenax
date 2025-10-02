using Core.DTOs;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class NotificationsFirebaseController : ControllerBase
{
    private readonly IFirebaseNotificationService _notificationService;

    public NotificationsFirebaseController(IFirebaseNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
    {
        var response = await _notificationService.SendNotificationAsync(request);

        if (response.Success)
            return Ok(response);
        else
            return BadRequest(response);
    }

    [HttpPost("send-to-topic")]
    public async Task<IActionResult> SendToTopic(
        [FromQuery] string topic,
        [FromBody] FirebaseNotification notification)
    {
        var response = await _notificationService.SendNotificationToTopicAsync(topic, notification);

        if (response.Success)
            return Ok(response);
        else
            return BadRequest(response);
    }

    //[HttpPost("send-multiple")]
    //public async Task<IActionResult> SendMultiple(
    //    [FromBody] List<string> deviceTokens,
    //    [FromBody] FirebaseNotification notification)
    //{
    //    var response = await _notificationService.SendNotificationToMultipleDevicesAsync(deviceTokens, notification);

    //    return Ok(response);
    //}
}