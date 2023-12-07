using ClaimAPI.Data;
using ClaimAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClaimAPI.Services
{
    public interface IAccountService
    {
        int Login(UserLogin user);
        UserVm GetUserByEmail(string email);
    }
    public class AccountService : IAccountService
    {
        ApplicationDbContext context { get; }
        public AccountService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int Login(UserLogin user)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "USP_LoginUser";
                command.CommandType = CommandType.StoredProcedure;
                var parameter = new SqlParameter("@email",user.Email);
                var parameter1 = new SqlParameter("@pass",user.Password);
                command.Parameters.Add(parameter);
                command.Parameters.Add(parameter1);
                context.Database.OpenConnection();
                int result=(int)command.ExecuteScalar();
                context.Database.CloseConnection();
                return result;
            }
        }

        public UserVm GetUserByEmail(string email)
        {
            using(var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_get_user_by_email";
                command.CommandType=CommandType.StoredProcedure;
                var parameter = new SqlParameter("@email", email);
                command.Parameters.Add(parameter);

                context.Database.OpenConnection();
                var result = command.ExecuteReader();
                result.Read();
                UserVm user = new UserVm();
                user.Email = result["Email"].ToString();
                user.Id = result["Id"].ToString();
                user.Name = result["Nm"].ToString();
                user.Mobile = result["Mobile"].ToString();
                user.Manager_Id = result["Manager_Id"].ToString();
                context.Database.CloseConnection();
                return user;
            }
        }
    }
}
