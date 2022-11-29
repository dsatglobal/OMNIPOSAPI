using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POS_API.Models.POSProducts;

namespace POS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class POSProductsController : Controller //Changed from Controller => ControllerBase
    {
        DALPOSProducts dalposproducts = new DALPOSProducts();       
        
        [HttpGet]
        public IActionResult List()
        {
            List<POSProducts> posproductsList = new List<POSProducts>();  
            posproductsList = dalposproducts.GetProductsOmnitoPOS();
            dalposproducts.AddProductsOmniToPos(posproductsList);
            return Ok(posproductsList);
        }
    }
}
