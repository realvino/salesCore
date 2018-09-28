using System.ComponentModel.DataAnnotations;
using tibs.stem.Authorization.Users;

namespace tibs.stem.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}