using LibraryManagementAPIInterface;
using LibraryManagementAPIModel.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Nexus.Controllers
{
    [Route("api/[controller]/[action]")]

    [ApiController]

    public class AuthController : Controller
    {
        private readonly IAuthenticate _auth;

        public AuthController(IAuthenticate auth)
        {
            this._auth = auth;
        }


        [HttpPost]
        public IActionResult AuthnticateUser([FromBody] LoginViewModel model)
        {
            var res = _auth.AutneticateUser(model);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();
        }

    }
}

