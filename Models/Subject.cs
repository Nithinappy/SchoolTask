
using SchoolTask.DTOs;

namespace SchoolTask.Models;
public record Subject
{
    public long Id { get; set; }
    public string Name { get; set; }



    public SubjectDTO asDto => new SubjectDTO
    {
        Id = Id,
        Name = Name,

    };
}