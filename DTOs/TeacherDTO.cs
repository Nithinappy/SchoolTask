
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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
    [JsonPropertyName("first_name")]
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string LastName { get; set; }
    [JsonPropertyName("gender")]
    [Required]
    public string Gender { get; set; }
    [JsonPropertyName("date_Of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }
    [JsonPropertyName("contact")]
    [Required]
    public long Contact { get; set; }
    [JsonPropertyName("subject_id")]
    [Required]
    public long SubjectId { get; set; }

}

public record TeacherUpdateDTO
{


    public string LastName { get; set; }

    public long Contact { get; set; }


}