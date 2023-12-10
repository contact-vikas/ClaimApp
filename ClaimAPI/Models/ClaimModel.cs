using System.Data;
using System;
using System.Diagnostics;
using System.Security.Policy;

namespace ClaimAPI.Models
{
    public class AddClaim
    {
        public string Claim_Title { get; set; }
        public string Claim_Reason { get; set; }
        public string Amount { get; set; }
        public string ExpenseDt { get;set; }  
        public string Evidence {  get; set; }  
        public string Claim_Description {  get; set; }  
        public int UserId {  get; set; }
    }

    public class PendingClaimRequest
    {
        public string Id { get; set; }
        public string Amount { get; set; }
        public string  Claim_Title { get; set; }
        public string  Claim_Reason { get; set; }
        public string  Claim_Description { get; set; }
        public string  ClaimDt { get; set; }
        public string  Evidence { get; set; }
        public string ExpenseDt { get; set; }
        public string CurrentStatus { get; set; }
        public string  Nm { get; set; }

    }

    public class ClaimAction
    {
        public string role { get; set; }
        public int action { get; set; }
        public string remark { get; set; }
        public int claimid { get; set; }
        public int userid { get; set; }

    }

    public class ClaimActionHistory
    {
        public string ActionDt {  get; set; }
        public string Action { get; set; }
        public string Nm { get; set; }
        public string Remarks { get; set; }
    }
}
