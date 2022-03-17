using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolTask.DTOs;

public record ClassRoomDTO
{


    [JsonPropertyName("classroom_id")]
    public long Id { get; set; }
    [JsonPropertyName("ClassRoom_name")]
    public string Name { get; set; }
    [JsonPropertyName("ClassRoom_students")]

    public List<StudentDTO> Students { get; set; }


}


public record ClassRoomCreateDTO
{


    [JsonPropertyName("Class_name")]
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; }
}

