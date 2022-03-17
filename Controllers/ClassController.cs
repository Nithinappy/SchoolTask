
using Microsoft.AspNetCore.Mvc;
using SchoolTask.DTOs;
using SchoolTask.Models;
using SchoolTask.Repositories;

namespace SchoolTask.Controllers;
[ApiController]
[Route("api/class_rooms")]
public class ClassRoomController : ControllerBase
{
    private readonly ILogger<ClassRoomController> _logger;
    private readonly IClassRoomRepository _classroom;
    private readonly IStudentRepository _student;
    // private readonly IClassRoomRepository _ClassRoom;
    public ClassRoomController(ILogger<ClassRoomController> logger, IClassRoomRepository classroom, IStudentRepository student)
    {
        _logger = logger;
        _student = student;
        _classroom = classroom;
    }
    [HttpGet]
    public async Task<ActionResult<List<ClassRoomDTO>>> GetAllClassRooms()
    {
        var ClassRoomList = await _classroom.GetAllClassRoom();

        var dtoList = ClassRoomList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpPost]
    public async Task<ActionResult<ClassRoomDTO>> CreateClassRoom([FromBody] ClassRoomCreateDTO Data)
    {
        var toCreateClassRoom = new ClassRoom
        {
            Name = Data.Name.Trim()
        };
        var createdClassRoom = await _classroom.CreateClassRoom(toCreateClassRoom);

        return StatusCode(StatusCodes.Status201Created, createdClassRoom.asDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClassRoomDTO>> GetClassRoomById([FromRoute] long id)
    {
        ClassRoomDTO ClassRoomDTO = new ClassRoomDTO();
        var ClassRoom = await _classroom.GetClassRoomById(id);

        if (ClassRoom is null)
            return NotFound("No ClassRoom found with given ClassRoom Id");

        ClassRoomDTO = ClassRoom.asDto;
        ClassRoomDTO.Students = (await _student.GetClassRoomStudentsById(id)).Select(x => x.asDto).ToList();

        return Ok(ClassRoomDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] long id)
    {
        var existing = await _classroom.GetClassRoomById(id);
        if (existing is null)
            return NotFound("No ClassRooms found with given ClassRoom Id");

        var didDelete = await _classroom.DeleteClassRoom(id);

        return NoContent();
    }



}
