
using Microsoft.AspNetCore.Mvc;
using SchoolTask.DTOs;
using SchoolTask.Models;
using SchoolTask.Repositories;

namespace SchoolTask.Controllers;
[ApiController]
[Route("api/students")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly IStudentRepository _student;
    private readonly ITeacherRepository _teacher;
    private readonly ISubjectRepository _subject;
    public StudentController(ILogger<StudentController> logger, IStudentRepository student,ITeacherRepository teacher,ISubjectRepository subject)
    {
        _logger = logger;
        _student = student;
        _subject=subject;
        _teacher = teacher;
        
    }
    [HttpGet]
    public async Task<ActionResult<List<StudentDTO>>> GetAllStudents([FromQuery] StudentParameter studentParameter)
    {
        var StudentsList = await _student.GetAllStudents(studentParameter);

        var dtoList = StudentsList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDTO>> GetStudentById([FromRoute] long id)
    {
        StudentDTO StudentDTO = new StudentDTO();
        var Student = await _student.GetStudentById(id);

        if (Student is null)
            return NotFound("No Student found with given Student Id");

        StudentDTO = Student.asDto;
        StudentDTO.Teachers = (await _teacher.GetTeachersById(id)).Select(x => x.asDto).ToList();
        StudentDTO.Subjects = (await _subject.GetStudentSubjectById(id)).Select(x => x.asDto).ToList();

        return Ok(StudentDTO);
    }

    [HttpPost]
    public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody] StudentCreateDTO Data)
    {


        var toCreateStudent = new Student
        {
            // first_name,last_name,Student_name,email,mobile,bio,address,passcode
            FirstName = Data.FirstName.Trim(),
            LastName = Data.LastName.Trim(),
            DateOfBirth=Data.DateOfBirth.UtcDateTime,
            Gender =Data.Gender,
            ParentContact=Data.ParentContact,
            ClassId=Data.ClassId
           
        };

        var createdStudent = await _student.CreateStudent(toCreateStudent);

        return StatusCode(StatusCodes.Status201Created, createdStudent.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStudent([FromRoute] long id,
    [FromBody] StudentUpdateDTO Data)
    {
        var existing = await _student.GetStudentById(id);
        if (existing is null)
            return NotFound("No Student found with given Student Id");

        var toUpdateStudent = existing with
        {

            ClassId = Data.ClassId ?? existing.ClassId,
            LastName = Data.LastName ?? existing.LastName,
            ParentContact =Data.ParentContact ?? existing.ParentContact

        };

        var didUpdate = await _student.UpdateStudent(toUpdateStudent);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Student");

        return NoContent();
    }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteStudent([FromRoute] long id)
    // {
    //     var existing = await _Student.GetStudentById(id);
    //     if (existing is null)
    //         return NotFound("No Student found with given Student Id");

    //     var didDelete = await _Student.UpdateStudent(id);

    //     return NoContent();
    // }



}
