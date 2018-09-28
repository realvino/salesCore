using System.ComponentModel.DataAnnotations;

namespace tibs.stem.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
