using ClaimAPI.Models;
using ClaimAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClaimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        APIResponse response = new APIResponse();
        IProgramService programservice { get; }
        public ProgramController(IProgramService programservice)
        {
            this.programservice = programservice;
        }

        [Route("GetProgramList")]
        [HttpGet]

        public IActionResult GetProgramList(int userid)
        {
            try
            {
                var result = programservice.GetAllPrograms(userid);
                response.status = 200;
                response.data = result;
                response.message = "Program found!";
                response.ok = true;
            }
            catch(Exception ex)
            {
                response.status = 500;
                response.message = ex.Message;
                response.ok = false;
            }
            return Ok(response);
        }
    }
}
