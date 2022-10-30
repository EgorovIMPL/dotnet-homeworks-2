using System.ComponentModel.DataAnnotations;
using Hw7.Enum;
using Hw7.ErrorMessages;

namespace Hw7.Models.ForTests;

public class TestModel : BaseModel
{
    [Required(ErrorMessage = Messages.RequiredMessage)]
    [MaxLength(30, ErrorMessage = $"First Name {Messages.MaxLengthMessage}")]
    public override string FirstName { get; set; } = null!;
    
    [MaxLength(30, ErrorMessage = $"Last Name {Messages.MaxLengthMessage}")]
    [Display(Name = "Фамилия")]
    public override string LastName { get; set; } = null!;
    
    [Required(ErrorMessage = Messages.RequiredMessage)]
    [Display(Name = "Отчество")]
    public override string? MiddleName { get; set; }
    
    [Range(10, 100, ErrorMessage = $"Age {Messages.RangeMessage}")]
    public override int Age { get; set; }
    
    [Display(Name = "Пол")]
    public override Sex Sex { get; set; }
    
    public string A { get; set; } = null!;
}