using Microsoft.AspNetCore.Mvc;
using POS_API.Models.POSUser;
using System.Collections.Generic;

namespace POS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class POSUserController : Controller
    {
        DALPOSUser dALPOSUser = new DALPOSUser();
        [HttpGet]
        public IActionResult Index()
        {
            List<POSUser> userlist=new List<POSUser>();
            userlist = dALPOSUser.GetPOSUserlistOmnitoPOS();
            dALPOSUser.AddUserOmniToPos(userlist);
            return Ok(userlist);
        }
    }
}
