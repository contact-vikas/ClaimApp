using ClaimAPI.Data;
using ClaimAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using System.Security.Policy;
using System.Security.Claims;

namespace ClaimAPI.Services
{
    public interface IClaimService
    {
        string AddClaimRequest(AddClaim claim);
        Tuple<string, List<PendingClaimRequest>> GetAllPendingRequest(int userid, string role);

        string ClaimAction(ClaimAction action);

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

        public Tuple<string, List<PendingClaimRequest>> GetAllPendingRequest(int userid, string role)
        {
            string message = string.Empty;
            List<PendingClaimRequest> lstPendingClaims = new List<PendingClaimRequest>();
            try
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "USP_GET_Pending_Request";
                    command.CommandType = CommandType.StoredProcedure;
                    var parameter = new SqlParameter();

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@userid";
                    parameter.SqlValue = userid;
                    parameter.SqlDbType = SqlDbType.Int;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@role";
                    parameter.SqlValue = role;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    context.Database.OpenConnection();
                    var result = command.ExecuteReader();
                    while (result.Read())
                    {
                        PendingClaimRequest claim = new PendingClaimRequest();
                        claim.Id = result["Id"].ToString();
                        claim.Amount = result["Amount"].ToString();
                        claim.Claim_Title = result["Claim_Title"].ToString();
                        claim.Claim_Reason = result["Claim_Reason"].ToString();
                        claim.Claim_Description = result["Claim_Description"].ToString();
                        claim.ClaimDt= result["ClaimDt"].ToString();
                        claim.Evidence= result["Evidence"].ToString();
                        claim.ExpenseDt= result["ExpenseDt"].ToString();
                        claim.CurrentStatus= result["CurrentStatus"].ToString();
                        claim.Nm= result["Nm"].ToString();

                        lstPendingClaims.Add(claim);
                    }
                        context.Database.CloseConnection();
                }
        }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return new Tuple<string,List<PendingClaimRequest>>(message, lstPendingClaims);
        }

        public string ClaimAction(ClaimAction action)
        {
            string message = string.Empty;
            try
            {
                using(var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "usp_update_claim";
                    command.CommandType = CommandType.StoredProcedure;

                    var parameter = new SqlParameter();

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@role";
                    parameter.SqlValue=action.role;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@action";
                    parameter.SqlValue = action.action;
                    parameter.SqlDbType = SqlDbType.Int;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@remark";
                    parameter.SqlValue = action.remark;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@claimid";
                    parameter.SqlValue = action.claimid;
                    parameter.SqlDbType = SqlDbType.Int;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@userid";
                    parameter.SqlValue = action.userid;
                    parameter.SqlDbType = SqlDbType.Int;
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);

                    context.Database.OpenConnection();
                    var result=command.ExecuteScalar();
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }
    }
}

