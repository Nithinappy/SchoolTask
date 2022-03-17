using SchoolTask.DTOs;

namespace SchoolTask.Models;



public record Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long ParentContact { get; set; }
    public long ClassId { get; set; }

    public StudentDTO asDto => new StudentDTO
    {

        Id = Id,
        FirstName = FirstName,
        LastName = LastName,
        ParentContact = ParentContact,
        ClassId = ClassId,
        Gender=Gender
    };
}