using System.ComponentModel.DataAnnotations;

namespace FirstWebAPiProject.Model.Dto
{
    public class StudentDto
    {
        [Required(ErrorMessage ="Forgate to Enter Name")]
        [StringLength(50,ErrorMessage ="Length Of the Name Goes Above 50 Character")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Forgate to Enter PhoneNumber")]
        [Range(10, 15, ErrorMessage = "PhoneNumber Should be Minimum 8 and Maximum 20 Character Long")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; } = null!;

        [Required(ErrorMessage = "Forgate to Enter PhoneNumber")]
        [Range(12, 20, ErrorMessage = "Age Should be Minimum 12y and Maximum 20y to Register ")]
        public int Age {  get; set; }

        [Required(ErrorMessage ="Forgate to Enter Password")]
        [Range(8,20,ErrorMessage ="PassWord Should be Minimum 8 and Maximum 20 Character Long")]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
