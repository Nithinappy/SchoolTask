using Dapper;
using SchoolTask.Repositories;
using SchoolTask.Models;
using SchoolTask.Utilities;


namespace SchoolTask.Repositories;
public interface ITeacherRepository
{
    Task<List<Teacher>> GetAllTeachers();
    Task<Teacher> GetTeacherById(long Id);
    Task<Teacher> CreateTeacher(Teacher item);
    Task<bool> UpdateTeacher(Teacher item);
    Task<Teacher> DeleteTeacher(long Id);
    Task<List<Teacher>> GetTeachersById(long Id);
    Task<List<Teacher>> GetSubjectTeacherById(long Id);


}

public class TeacherRepository : BaseRepository, ITeacherRepository
{

    public TeacherRepository(IConfiguration config) : base(config)
    {

    }
    public async Task<Teacher> CreateTeacher(Teacher item)
    {
        var query = $@"INSERT INTO ""{TableNames.teacher}"" 
        (first_name,last_name,gender,date_of_birth,contact,subject_id) 
        VALUES (@FirstName, @LastName, @Gender ,@DateOfBirth, @Contact,@SubjectId) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Teacher>(query, item);
            return res;
        }
    }

    public Task<Teacher> DeleteTeacher(long Id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Teacher>> GetAllTeachers()
    {

        // Query
        var query = $@"SELECT * FROM ""{TableNames.teacher}""";

        List<Teacher> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Teacher>(query)).AsList();

        return res;
    }

    public async Task<List<Teacher>> GetSubjectTeacherById(long Id)
    {
         var query = $@"SELECT * FROM {TableNames.teacher} WHERE subject_id = @Id";

        using (var con = NewConnection)
           return (await con.QueryAsync<Teacher>(query,new {Id})).AsList();
    }

    public async Task<Teacher> GetTeacherById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.teacher}"" 
        WHERE id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Teacher>(query, new { Id });

    }

    public async Task<List<Teacher>> GetTeachersById(long Id)
    {
        var query = $@"SELECT t.*, s.name AS subject_name FROM {TableNames.student_teacher} st 
        LEFT JOIN {TableNames.teacher} t ON t.id = st.teacher_id
        LEFT JOIN {TableNames.subject} s ON s.id = t.subject_id
        WHERE st.student_id = @Id";

        using (var con = NewConnection)
           return (await con.QueryAsync<Teacher>(query,new {Id})).AsList();
    }

    public async Task<bool> UpdateTeacher(Teacher item)
    {
        var query = $@"UPDATE ""{TableNames.teacher}"" SET 
        last_name = @LastName,contact = @Contact WHERE Id = @Id";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }


    }


    // public async Task<List<Teacher>> GetPostCreatedBy(long PostId)
    // {
    //     var query = $@"SELECT u.* FROM {TableNames.posts}  p LEFT JOIN {TableNames.Teachers} u 
    // 		  on p.Id = u.Id  WHERE post_id= @PostId";
    //     List<Teacher> res;
    //     using (var con = NewConnection) // Open connection
    //         res = (await con.QueryAsync<Teacher>(query, new { PostId })).AsList(); // Execute the query

    //     return res; ;

    // }
}