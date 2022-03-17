using SchoolTask.DTOs;

namespace SchoolTask.Models;
public record ClassRoom
{
    public long Id { get; set; }
    public string Name { get; set; }



    public ClassRoomDTO asDto => new ClassRoomDTO
    {
        Name = Name,
        Id =Id

    };
}