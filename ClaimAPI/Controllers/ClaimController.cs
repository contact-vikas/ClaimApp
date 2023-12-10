using ClaimAPI.Models;
using ClaimAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ClaimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        public IClaimService claimService { get; }
        private APIResponse response = new APIResponse();

        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        public ClaimController(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IClaimService _claimService)
        {
            Environment = environment;
            claimService = _claimService;
        }

        [Route("RaiseClaim")]
        [HttpPost]

        public IActionResult RaiseClaim()
        {
            try
            {
                var evidence = Request.Form.Files[0];
                AddClaim claim = new AddClaim();
                claim.Claim_Title = Request.Form["ClaimTitle"].ToString();
                claim.Claim_Reason = Request.Form["ClaimReason"].ToString() ;
                claim.Claim_Description = Request.Form["ClaimDescription"].ToString() ;
                claim.Amount = Request.Form["ClaimAmount"].ToString() ; 
                claim.ExpenseDt = Request.Form["ExpenseDt"].ToString() ;
                claim.UserId =Convert.ToInt32( Request.Form["UserId"]) ;
                claim.Evidence = UploadEvidence(evidence);
                string result=claimService.AddClaimRequest(claim);
                if(string.IsNullOrEmpty(result))
                {
                    response.status = 200;
                    response.ok = true;
                    response.message = "Claim Requested Successfully";
                }
                else
                {
                    response.status = -100;
                    response.ok = false;
                    response.message = result;
                }
            }
            catch(Exception ex)
            {
                response.status = -100;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [Route("GetPendingClaims")]
        [HttpGet]

        public IActionResult GetPendingClaims(int userid,string role)
        {
            try
            {
                
                var result=claimService.GetAllPendingRequest(userid,role);
                if(result.Item1.IsNullOrEmpty())
                {

                response.status = 200;
                response.ok = true;
                response.data = result.Item2;
                response.message = "Record Found";
                }
                else
                {

                response.status= -100;
                response.ok = false;
                    response.message = result.Item1;
                }
            }
            catch (Exception ex)
            {
                response.status = -100;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [Route("ActionClaim")]
        [HttpPost]

        public IActionResult ActionClaim(ClaimAction action)
        {
            try
            {
                string result=claimService.ClaimAction(action);
                if (string.IsNullOrEmpty(result))
                {

                response.ok=true;
                response.status = 200;
                    response.message = "Claim updated successfully!";
                }
                else
                {
                    response.status = -100;
                    response.ok = false;
                    response.data = null;
                    response.message = result;

                }
            }
            catch(Exception ex) {
                response.status = -100;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [Route("GetClaimActionHistory")]
        [HttpGet]

        public IActionResult GetClaimActionHistory(int claimid)
        {
            var result=claimService.GetClaimActionHistory(claimid);
            try
            {
                if (string.IsNullOrEmpty( result.Item1))
                {
                    response.status=200;
                    response.ok = true;
                    response.data = result.Item2;
                    response.message = "Claim Action fetched successfully";
                }
                else
                {
                    response.status = -100;
                    response.ok = false;
                    response.message = "Claim Action not fetched successfully";
                }
            }
            catch(Exception ex)
            {
                response.status = -100;
                response.ok = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        private string UploadEvidence(IFormFile file)
        {
            var rootpath = Environment.ContentRootPath;
            var ext = Path.GetExtension(file.FileName);
            string filename="Evidence"+Guid.NewGuid().ToString()+ext;
            var fullpath=Path.Combine(rootpath,"EvidenceFiles",filename);
            using(FileStream stream=new FileStream(fullpath, FileMode.Create))
            {
                file.CopyTo(stream);
                return "EvidenceFiles/" + filename;
            }
        }
    }
}
