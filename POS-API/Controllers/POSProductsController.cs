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
    public class POSProductsController : ControllerBase //Changed from Controller => ControllerBase
    {
        //Class Name : POSProducts.cs

        POSProducts posproducts = new POSProducts();
        DALPOSProducts dalposproducts = new DALPOSProducts();



        //***********************************************************************************************
        //                      Action Name : [ List  ]
        //***********************************************************************************************
        //Folder Name       : POSProducts
        //Model Class Name  : POSProducts.cs
        //DAL Class Name    : DALPOSProducts.cs
        //DAL Method Name   : [GetProductsOmnitoPOS] for Select and  [ AddProductsOmniToPos ] for Insert 
        //Parameter Name    : [ "SupplierId" ]
        [HttpGet]
        public IActionResult List(int SupplierId)
        {
            List<POSProducts> posproductsList = new List<POSProducts>();           
            posproductsList = dalposproducts.GetProductsOmnitoPOS(SupplierId);
            dalposproducts.AddProductsOmniToPos(posproductsList);
            return Ok();
        }
    }
}
