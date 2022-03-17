
using Microsoft.AspNetCore.Mvc;
using SchoolTask.DTOs;
using SchoolTask.Models;
using SchoolTask.Repositories;

namespace SchoolTask.Controllers;
[ApiController]
[Route("api/teachers")]
public class TeacherController : ControllerBase
{
    private readonly ILogger<TeacherController> _logger;
    private readonly ITeacherRepository _teacher;
    private readonly ISubjectRepository _subject;
      private readonly IStudentRepository _student;
    public TeacherController(ILogger<TeacherController> logger, ITeacherRepository teacher,IStudentRepository student,ISubjectRepository subject )
    {
        _logger = logger;
        _teacher = teacher;
       _subject = subject;
       _student=student;
    }
    [HttpGet]
    public async Task<ActionResult<List<TeacherDTO>>> GetAllTeachers()
    {
        var TeachersList = await _teacher.GetAllTeachers();

        var dtoList = TeachersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherDTO>> GetTeacherById([FromRoute] long id)
    {
        TeacherDTO TeacherDTO = new TeacherDTO();
        var Teacher = await _teacher.GetTeacherById(id);

        if (Teacher is null)
            return NotFound("No Teacher found with given Teacher Id");

        TeacherDTO = Teacher.asDto;
        // TeacherDTO.Subjects = (await _subject.GetTeacherSubjectsById(id)).Select(x => x.asDto).ToList();
        TeacherDTO.Students = (await _student.GetTeacherStudentsById(id)).Select(x => x.asDto).ToList();

        return Ok(TeacherDTO);
    }

    [HttpPost]
    public async Task<ActionResult<TeacherDTO>> CreateTeacher([FromBody] TeacherCreateDTO Data)
    {
        var toCreateTeacher = new Teacher
        {
            FirstName = Data.FirstName.Trim(),
            LastName = Data.LastName.Trim(),
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Gender = Data.Gender,
            SubjectId = Data.SubjectId,
            Contact = Data.Contact
        };

        var createdTeacher = await _teacher.CreateTeacher(toCreateTeacher);

        return StatusCode(StatusCodes.Status201Created, createdTeacher.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTeacher([FromRoute] long id,
    [FromBody] TeacherUpdateDTO Data)
    {
        var existing = await _teacher.GetTeacherById(id);
        if (existing is null)
            return NotFound("No Teacher found with given Teacher Id");

        var toUpdateTeacher = existing with
        {
            LastName = Data.LastName ?? existing.LastName,
            Contact = Data.Contact
        };

        var didUpdate = await _teacher.UpdateTeacher(toUpdateTeacher);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Teacher");

        return NoContent();
    }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteTeacher([FromRoute] long id)
    // {
    //     var existing = await_teacher.GetTeacherById(id);
    //     if (existing is null)
    //         return NotFound("No Teacher found with given Teacher Id");

    //     var didDelete = await_teacher.UpdateTeacher(id);

    //     return NoContent();
    // }



}
