using Microsoft.AspNetCore.Mvc;
using POS_API.Models.Customer;
using System.Collections.Generic;

namespace POS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : Controller
    {
        DALCustomer dALCustomer= new DALCustomer();
        [HttpGet]
        public IActionResult Index()
        {
            List<Customer> customerList = new List<Customer>();
            customerList = dALCustomer.GetCustomerOmnitoPOS();
            dALCustomer.AddCustomerOmniToPos(customerList);
            return Ok(customerList);
        }
    }
}
