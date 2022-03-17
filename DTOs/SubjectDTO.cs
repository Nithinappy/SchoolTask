using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolTask.DTOs;

public record SubjectDTO
{
    [JsonPropertyName("subject_id")]
    public long Id { get; set; }

    [JsonPropertyName("subject_name")]

    public string Name { get; set; }
    public List<TeacherDTO> Teachers { get; set; }


}


public record SubjectCreateDTO
{

    [JsonPropertyName("subject_name")]
    [Required]
    public string Name { get; set; }
}

