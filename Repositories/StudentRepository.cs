using Dapper;
using SchoolTask.Repositories;
using SchoolTask.Models;
using SchoolTask.Utilities;
using SchoolTask.Helpers;

namespace SchoolTask.Repositories;
public interface IStudentRepository
{
    Task<List<Student>> GetAllStudents(StudentParameter studentParameter);
    Task<Student> GetStudentById(long Id);
    Task<Student> CreateStudent(Student item);
    Task<bool> UpdateStudent(Student item);
    Task<Student> DeleteStudent(long Id);

    Task<List<Student>> GetClassRoomStudentsById(long Id);
    Task<List<Student>> GetTeacherStudentsById(long Id);



}

public class StudentRepository : BaseRepository, IStudentRepository
{

    public StudentRepository(IConfiguration config) : base(config)
    {

    }
    public async Task<Student> CreateStudent(Student item)
    {
        var query = $@"INSERT INTO ""{TableNames.student}"" 
        (first_name,last_name,gender,date_of_birth,parent_contact,class_id) 
        VALUES (@FirstName, @LastName, @Gender ,@DateOfBirth, @ParentContact,@ClassId) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Student>(query, item);
            return res;
        }
    }

    public Task<Student> DeleteStudent(long Id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Student>> GetAllStudents(StudentParameter studentParameter)
    {

        // Query
        var query = $@"SELECT * FROM ""{TableNames.student}""";

        List<Student> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Student>(query))
            .Skip((studentParameter.PageNumber - 1) * studentParameter.PageSize)
            .Take(studentParameter.PageSize)
            .AsList();

        return res;
    }


    public async Task<Student> GetStudentById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.student}"" 
        WHERE id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Student>(query, new { Id });

    }

    public async Task<bool> UpdateStudent(Student item)
    {
        var query = $@"UPDATE ""{TableNames.student}"" SET class_id = @ClassId, 
        last_name = @LastName,parent_contact = @ParentContact WHERE Id = @Id";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }


    }

    public async Task<List<Student>> GetClassRoomStudentsById(long Id)
    {
        var query = $@"SELECT * FROM {TableNames.student} WHERE class_id = @Id";

        using (var con = NewConnection)
            return (await con.QueryAsync<Student>(query)).AsList();
    }

    public async Task<List<Student>> GetTeacherStudentsById(long Id)
    {
        var query = $@"SELECT * FROM {TableNames.student_teacher} st
        LEFT JOIN {TableNames.student} s ON s.id = st.student_id 
            WHERE st.teacher_id = @Id";

        using (var con = NewConnection)
            return (await con.QueryAsync<Student>(query, new { Id })).AsList();
    }


}