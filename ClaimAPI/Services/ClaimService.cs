using ClaimAPI.Data;
using ClaimAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using System.Security.Policy;

namespace ClaimAPI.Services
{
    public interface IClaimService
    {
        string AddClaimRequest(AddClaim claim);
    }
    public class ClaimService : IClaimService
    {
        ApplicationDbContext context { get; }
        public ClaimService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public string AddClaimRequest(AddClaim claim)
        {
            string message = string.Empty;
            try
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "USP_Raise_Claim_Request";
                    command.CommandType = CommandType.StoredProcedure;

                    var parameter = new SqlParameter();

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@Claim_Reason";
                    parameter.SqlValue = claim.Claim_Reason;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@Claim_Title";
                    parameter.SqlValue = claim.Claim_Title;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);


                    parameter = new SqlParameter();
                    parameter.ParameterName = "@Amount";
                    parameter.SqlValue = Convert.ToDecimal(claim.Amount);
                    parameter.SqlDbType = SqlDbType.Decimal;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@ExpenseDt";
                    parameter.SqlValue = claim.ExpenseDt;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@Evidence";
                    parameter.SqlValue = claim.Evidence;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@Claim_Description";
                    parameter.SqlValue = claim.Claim_Description;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@UserId";
                    parameter.SqlValue = claim.UserId;
                    parameter.SqlDbType = SqlDbType.Int;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    context.Database.OpenConnection();
                    var result = command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;

            }
        }
 }

