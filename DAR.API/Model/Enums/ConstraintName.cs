using System.ComponentModel.DataAnnotations;

namespace DAR.API.Model.Enums
{
    public enum ConstraintName
    {
        [Display(Name = "min")]
        Min,
        [Display(Name = "max")]
        Max

    }
}