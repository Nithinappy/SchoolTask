using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


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
    [JsonPropertyName("parent_contact")]
    [Required]
    public long ParentContact { get; set; }
    [Required]
    public long ClassId { get; set; }


}

public record StudentUpdateDTO
{


    public string LastName { get; set; }

    public long? ParentContact { get; set; }
    public long? ClassId { get; set; }

}