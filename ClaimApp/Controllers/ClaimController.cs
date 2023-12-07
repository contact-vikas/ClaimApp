using Microsoft.AspNetCore.Mvc;

namespace ClaimApp.Controllers
{
    public class ClaimController : Controller
    {
        public IActionResult AddClaim()
        {
            return View();
        }
    }
}
