using Abp.Notifications;
using tibs.stem.Dto;

namespace tibs.stem.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}