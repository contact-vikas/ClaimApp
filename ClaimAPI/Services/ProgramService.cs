using ClaimAPI.Data;
using ClaimAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClaimAPI.Services
{
    public interface IProgramService
    {
        List<ProgramModel> GetAllPrograms(int userid);
    }

    public class ProgramService : IProgramService
    {
        ApplicationDbContext context { get; }
        public ProgramService(ApplicationDbContext context) 
        {
          this.context = context;
        }
        public List<ProgramModel> GetAllPrograms(int userid)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usd_get_program_path";
                command.CommandType = CommandType.StoredProcedure;
                var parameter = new SqlParameter("@UserId", userid);
                command.Parameters.Add(parameter);
                context.Database.OpenConnection();
                var result=command.ExecuteReader();
                List<ProgramModel> programs = new List<ProgramModel>();
                while (result.Read())
                {
                    ProgramModel program = new ProgramModel();
                program.Id = result["Id"].ToString();
                program.title = result["title"].ToString();
                program.Descr = result["Descr"].ToString();
                program.path = result["path"].ToString();
                    programs.Add(program);
                }
                context.Database.CloseConnection();
                return programs;
            }
        }
    }
}
