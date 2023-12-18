using System.ComponentModel.DataAnnotations;

namespace FirstWebAPiProject.Model.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string PhoneNo { get; set; } = null!;
    }
}
