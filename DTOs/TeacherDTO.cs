

namespace SchoolTask.DTOs;



public record TeacherDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public long Contact { get; set; }
    public long SubjectId { get; set; }
      public List<StudentDTO> Students { get; set; }
    // public List<SubjectDTO> Subjects { get; set; }


}






public record TeacherCreateDTO
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long Contact { get; set; }
    public long SubjectId { get; set; }

}

public record TeacherUpdateDTO
{


    public string LastName { get; set; }

    public long Contact { get; set; }


}