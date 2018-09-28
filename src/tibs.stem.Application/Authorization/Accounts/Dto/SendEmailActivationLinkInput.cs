using System.ComponentModel.DataAnnotations;

namespace tibs.stem.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}