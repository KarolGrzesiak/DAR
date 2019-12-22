using System.ComponentModel.DataAnnotations;

namespace DAR.API.Model.Enums
{
    public enum ValuesTypeName
    {
        [Display(Name = "long")]
        Numeric,
        [Display(Name = "boolean")]
        Boolean,
        [Display(Name = "string")]
        String
    }
}