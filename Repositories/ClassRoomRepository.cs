using Dapper;
using SchoolTask.Repositories;
using SchoolTask.Models;
using SchoolTask.Utilities;

namespace SchoolTask.Repositories;
public interface IClassRoomRepository
{


    Task<ClassRoom> CreateClassRoom(ClassRoom item);
    Task<ClassRoom> GetClassRoomById(long Id);
    Task<List<ClassRoom>> GetAllClassRoom();
    

    Task<bool> DeleteClassRoom(long Id);

}

public class ClassRoomRepository : BaseRepository, IClassRoomRepository
{

    public ClassRoomRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<ClassRoom> CreateClassRoom(ClassRoom item)
    {
        var query = $@"INSERT INTO ""{TableNames.class_room}"" 
        (name) 
        VALUES (@Name)  RETURNING *";
        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<ClassRoom>(query, item);
            return res;
        }
    }

    public async Task<bool> DeleteClassRoom(long Id)
    {
        var query = $@"Delete  FROM ""{TableNames.class_room}"" 
        WHERE id = @Id";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { Id });
            return res > 0;
        }

    }

    public async Task<List<ClassRoom>> GetAllClassRoom()
    {
        // Query
        var query = $@"SELECT * FROM {TableNames.class_room}";

        List<ClassRoom> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<ClassRoom>(query)).AsList();
        return res;
    }

    public async Task<ClassRoom> GetClassRoomById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.class_room}"" WHERE id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<ClassRoom>(query, new { Id });

    }

   

    public Task<List<ClassRoom>> GetPostClassRoomById(long PostId)
    {
        throw new NotImplementedException();
    }

    
    // public async Task<List<ClassRoom>> GetPostClassRoomById(long PostId)
    // {
    //     var query = $@"SELECT ht.* FROM {TableNames.ClassRoom_room}  hp LEFT JOIN {TableNames.ClassRoom} ht 
    // 		  on hp.hash_tag_id = ht.hash_tag_id  WHERE post_id = @PostId ORDER BY ht.hash_tag_id ASC";
    //     List<ClassRoom> res;
    //     using (var con = NewConnection) // Open connection
    //         res = (await con.QueryAsync<ClassRoom>(query, new { PostId })).AsList(); // Execute the query

    //     return res;
    // }


}