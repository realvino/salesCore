using System.ComponentModel.DataAnnotations;

namespace tibs.stem.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}