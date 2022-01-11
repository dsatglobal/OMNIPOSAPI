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
        //Note : ProductsTYPE table consist of 13 columns
        //Note : Order of 13 Columns is : ProductCode,ProductDesc,ManufacturerName,Model_Name,Category,SubCategory,CostPrice,WholeSalePrice,RetailPrice,MinStock,MaxStock,SafetyStock,SupplierName


        //1 ) Product Code
        public string ProductCode { get; set; }

        //2 ) ProductDesc
        public string ProductDesc { get; set; }

        //3 ) ManufacturerName
        public string ManufacturerName { get; set; }

        //4 ) Model_Name
        public string Model_Name { get; set; }


        //5 ) Category
        public string Category { get; set; }


        //6 ) SubCategory
        public string SubCategory { get; set; }

        //7 ) CostPrice
        public decimal CostPrice { get; set; }

        //8 ) WholeSalePrice
        public decimal WholeSalePrice { get; set; }

        //9 ) RetailPrice
        public decimal RetailPrice { get; set; }


        //10 ) MinStock
        public int MinStock { get; set; }


        //11 ) MaxStock
        public int MaxStock { get; set; }


        //12 ) SafetyStock
        public int SafetyStock { get; set; }


        //13 ) SupplierName
        public string SupplierName { get; set; }

        
        
        //14) StockInHand  --Newly added for Quantity purpose

        public int StockInHand { get; set; }


        
    }


  
}
