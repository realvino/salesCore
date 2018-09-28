using System.Collections.Generic;

namespace tibs.stem.Web.Models.TokenAuth
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public bool ShouldResetPassword { get; set; }

        public string PasswordResetCode { get; set; }

        public long UserId { get; set; }

        public bool RequiresTwoFactorVerification { get; set; }

        public IList<string> TwoFactorAuthProviders { get; set; }

        public string TwoFactorRememberClientToken { get; set; }

        public string ReturnUrl { get; set; }

        public int RootId { get; set; }
    }
    public class AuthenticateTenentResultModel
    {
        public int UserId { get; set; }
        public int TenentId { get; set; }
        public string TenentName { get; set; }

    }
}