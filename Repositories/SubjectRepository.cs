using Dapper;
using SchoolTask.Repositories;
using SchoolTask.Models;
using SchoolTask.Utilities;

namespace SchoolTask.Repositories;
public interface ISubjectRepository
{


    Task<Subject> CreateSubject(Subject item);
    Task<Subject> GetSubjectById(long Id);
    Task<List<Subject>> GetAllSubject();
    Task<List<Subject>> GetStudentSubjectById(long Id);
    Task<List<Subject>> GetTeacherSubjectsById(long Id);

    Task<bool> DeleteSubject(long Id);

}

public class SubjectRepository : BaseRepository, ISubjectRepository
{

    public SubjectRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Subject> CreateSubject(Subject item)
    {
        var query = $@"INSERT INTO ""{TableNames.subject}"" 
        (name) 
        VALUES (@Name)  RETURNING *";
        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Subject>(query, item);
            return res;
        }
    }

    public async Task<bool> DeleteSubject(long Id)
    {
        var query = $@"Delete  FROM ""{TableNames.subject}"" 
        WHERE id = @Id";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { Id });
            return res > 0;
        }

    }

    public async Task<List<Subject>> GetAllSubject()
    {
        // Query
        var query = $@"SELECT * FROM ""{TableNames.subject}""";

        List<Subject> res;
        using (var con = NewConnection) 
            res = (await con.QueryAsync<Subject>(query)).AsList(); // Execute the query
      
        return res;
    }

    public async Task<List<Subject>> GetStudentSubjectById(long Id)
    {
        var query = $@"SELECT * FROM {TableNames.student_subject} ss 
        LEFT JOIN {TableNames.subject} s ON s.id = ss.subject_id
         WHERE ss.student_id = @Id";

        using (var con = NewConnection)
           return (await con.QueryAsync<Subject>(query,new {Id})).AsList();
    }

    public async Task<Subject> GetSubjectById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.subject}"" 
        WHERE id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Subject>(query, new { Id });

    }

    public async Task<List<Subject>> GetTeacherSubjectsById(long Id)
    {
        var query = $@"SELECT * FROM {TableNames.teacher} WHERE subject_id = @Id";

        using (var con = NewConnection)
           return (await con.QueryAsync<Subject>(query,new {Id})).AsList();
    }






    // public async Task<List<Subject>> GetPostSubjectById(long PostId)
    // {
    //     var query = $@"SELECT ht.* FROM {TableNames.Subject_room}  hp LEFT JOIN {TableNames.Subject} ht 
    // 		  on hp.hash_tag_id = ht.hash_tag_id  WHERE post_id = @PostId ORDER BY ht.hash_tag_id ASC";
    //     List<Subject> res;
    //     using (var con = NewConnection) // Open connection
    //         res = (await con.QueryAsync<Subject>(query, new { PostId })).AsList(); // Execute the query

    //     return res;
    // }


}