using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Models.POSProducts
{
    public class POSProducts
    {

        //Note : Always the Columns of User Defined Table Types should match with the Object declared in the ModelClass.Cs file.
        //Note : User Defined Table Type Name : ProductsTYPE [ Backend ]
        //Note : OmniToPOSProductsTYPE  table consist of 19 columns

        public string ProductCode { get; set; }
        public string ProductDesc { get; set; }
        public string ManufacturerName { get; set; }
        public decimal RetailPrice { get; set; }
        public int StockInHand { get; set; }
        public string MerchantName { get; set; }
        public string MContactName { get; set; }
        public string MAddress { get; set; }
        public string MPhone { get; set; }
        public string MMobile { get; set; }
        public string MEmail { get; set; }
        public string MCity { get; set; }
        public string MCountry { get; set; }
        public string MVATNumber { get; set; }
        public string StoreName { get; set; }
        public string SContactName { get; set; }
        public string SAddress { get; set; }
        public string SContactNumber { get; set; }
        public string SEmail { get; set; }
        public string SCity { get; set; }
        public string SCountry { get; set; }
    }
}
