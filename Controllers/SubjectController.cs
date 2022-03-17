
using Microsoft.AspNetCore.Mvc;
using SchoolTask.DTOs;
using SchoolTask.Models;
using SchoolTask.Repositories;

namespace SchoolTask.Controllers;
[ApiController]
[Route("api/subjects")]
public class SubjectController : ControllerBase
{
    private readonly ILogger<SubjectController> _logger;
    private readonly ISubjectRepository _subject;
   private readonly ITeacherRepository _teacher;
   private readonly IStudentRepository _student;
    public SubjectController(ILogger<SubjectController> logger, ISubjectRepository subject,IStudentRepository student,ITeacherRepository teacher)
    {
        _logger = logger;

        _subject = subject;
        _student= student;
        _teacher=teacher;
    }
    [HttpGet]
    public async Task<ActionResult<List<SubjectDTO>>> GetAllSubjects()
    {
        var SubjectList = await _subject.GetAllSubject();

        var dtoList = SubjectList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpPost]
    public async Task<ActionResult<SubjectDTO>> CreateSubject([FromBody] SubjectCreateDTO Data)
    {


        var toCreateSubject = new Subject
        {
            Name = Data.Name.Trim()
        };

        var createdSubject = await _subject.CreateSubject(toCreateSubject);

        return StatusCode(StatusCodes.Status201Created, createdSubject.asDto);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectDTO>> GetSubjectById([FromRoute] long id)
    {
        SubjectDTO SubjectDTO = new SubjectDTO();
        var Subject = await _subject.GetSubjectById(id);

        if (Subject is null)
            return NotFound("No Subject found with given Subject Id");

        SubjectDTO = Subject.asDto;
        SubjectDTO.Teachers = (await _teacher.GetSubjectTeacherById(id)).Select(x => x.asDto).ToList();

        return Ok(SubjectDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] long id)
    {
        var existing = await _subject.GetSubjectById(id);
        if (existing is null)
            return NotFound("No Subjects found with given Subject Id");

        var didDelete = await _subject.DeleteSubject(id);

        return NoContent();
    }



}
