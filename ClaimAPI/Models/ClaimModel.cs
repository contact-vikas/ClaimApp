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
}
