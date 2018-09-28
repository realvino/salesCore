using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using tibs.stem.MultiTenancy.Payments;
using tibs.stem.MultiTenancy.Payments.Dto;

namespace tibs.stem.MultiTenancy.Dto
{
    public class RegisterTenantInput
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }

        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string AdminPassword { get; set; }

        [DisableAuditing]
        public string CaptchaResponse { get; set; }

        public SubscriptionStartType SubscriptionStartType { get; set; }

        public SubscriptionPaymentGatewayType? Gateway { get; set; }

        public int? EditionId { get; set; }

        public string PaymentId { get; set; }
        public virtual int? TenantTypeId { get; set; }
    }
    public class EmailCheck
    {
        public string Email { get; set; }
    }
    public class CheckEmailValid
    {
        public string email { get; set; }
    }
}