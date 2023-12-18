using FirstWebAPiProject.Model.Dto;

namespace FirstWebAPiProject.Data
{
    public class StudentStore
    {
        public static List<StudentDto> studentList = new List<StudentDto>()
        {
            new StudentDto() { Id=1,Name="Govind",PhoneNo="9832343293" },
            new StudentDto() { Id=2,Name="Rishab",PhoneNo="8822940284"}
        };
    }
}
