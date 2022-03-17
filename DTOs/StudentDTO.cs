

namespace SchoolTask.DTOs;



public record StudentDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }

    public long ParentContact { get; set; }
    public long ClassId { get; set; }
    public List<TeacherDTO> Teachers { get; set; }
    public List<SubjectDTO> Subjects { get; set; }


}






public record StudentCreateDTO
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long ParentContact { get; set; }
    public long ClassId { get; set; }


}

public record StudentUpdateDTO
{


    public string LastName { get; set; }

    public long? ParentContact { get; set; }
    public long? ClassId { get; set; }

}