using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;  //Newly added using statements
using System.Data;
using System.Reflection;

namespace POS_API.Models.POSProducts
{
    public class DALPOSProducts
    {
        //This class will contain our Database related operations.
        // Step 1 : Connection String Declarations:
        //------------------------------------------------------------------------------
        // String Name      : connectionstring
        // Class Name       : GetConString ( File Name : GetConString.cs)
        // Method Name      : ConString() ( File Name : GetConString.cs)

        public string connectionstring { get; set; }

        public DALPOSProducts()
        {
            connectionstring = GetConString.ConString();
        }

        //For ListToDataTableConveter:

        ListtoDataTableConverter listdata = new ListtoDataTableConverter();

        //=====================================================================================================================================

        //Step 2 : To To Get all Products from Omnicore database to POS Database
        //----------------------------------------------------------------------------------------------------
        //Modal Class name      : POSProducts      (File name : POSProducts.cs)
        //Modal Object name     : obj_posproducts
        //Method name           : GetProductsOmnitoPOS  
        //Procdeure name        : [ SP_GetProductsOmnitoPosAPI ]
        //Parameter name        : [ "SupplierId" ]


        //Note : The parameter SupplierName was newly added for ProductMapping Screen Purpose

        public List<POSProducts> GetProductsOmnitoPOS()
        {
            List<POSProducts> posproductsList = new List<POSProducts>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("SP_GetProductsOmnitoPosAPI", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    POSProducts obj_posproducts = new POSProducts();
                    obj_posproducts.ProductCode = rdr["ProductCode"].ToString();
                    obj_posproducts.ProductDesc = rdr["ProductDesc"].ToString();
                    obj_posproducts.ManufacturerName = rdr["PartManufacturerName"].ToString();
                    obj_posproducts.FranchiseeName = rdr["ManufacturerName"].ToString();
                    obj_posproducts.RetailPrice = Convert.ToDecimal(rdr["RetailPrice"]);
                    obj_posproducts.StockInHand = Convert.ToInt32(rdr["StockInHand"]);
                    obj_posproducts.MerchantName = rdr["MerchantName"].ToString();
                    obj_posproducts.MContactName = rdr["MContactName"].ToString();
                    obj_posproducts.MAddress = rdr["MAddress"].ToString();
                    obj_posproducts.MPhone = rdr["MPhone"].ToString();
                    obj_posproducts.MEmail = rdr["MEMail"].ToString();
                    obj_posproducts.MMobile = rdr["MMobile"].ToString();
                    obj_posproducts.MCity = rdr["MCity"].ToString();
                    obj_posproducts.MCountry = rdr["MCountry"].ToString();
                    obj_posproducts.MVATNumber = rdr["VATNumber"].ToString();
                    obj_posproducts.StoreName = rdr["StoreName"].ToString();
                    obj_posproducts.SContactName = rdr["SContactName"].ToString();
                    obj_posproducts.SAddress = rdr["SAddress"].ToString();
                    obj_posproducts.SContactNumber = rdr["SContactNumber"].ToString();
                    obj_posproducts.SEmail = rdr["SEMail"].ToString();
                    obj_posproducts.SCity = rdr["SCity"].ToString();
                    obj_posproducts.SCountry = rdr["SCountry"].ToString();
                    posproductsList.Add(obj_posproducts);
                }
                con.Close();
            }
            return posproductsList;
        }



        //=====================================================================================================================================

        //Step 3 : To To Insert the retrieved data from Omnicore database into POS Database
        //----------------------------------------------------------------------------------------------------
        //Modal Class name      : POSProducts      (File name : POSProducts.cs)
        //Modal Object name     : obj_posproducts
        //Method name           : GetProductsOmnitoPOS  
        //Procdeure name        : [ SP_InsertProductOmnitoPos ]
        //Parameter name        : [ "posproductsList" ]- Here we pass entire list as a parameter which is the return objects of  GetProductsOmnitoPOS method
        //Parameter name        : [ "tblProductsType" ] -This parameter is for the stored procedure -SP_InsertProductOmnitoPos. 
        public void AddProductsOmniToPos(List<POSProducts> posproductsList)
        {
            try { 
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertProductOmnitoPos", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    DataTable dt1 = new DataTable();
                    dt1 = listdata.ToDataTable<POSProducts>(posproductsList);

                    cmd.Parameters.AddWithValue("@tblProductsType", dt1);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex) 
            { 
            
            }
        }

    }
    //===========================================================================================================================================================

    //Newly added Class for API Implementations
    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
    //================================================================================================================================
}


//***************************************************************************************************************************************
//                                              SQL Exceptions Faced & Its Solutions
//****************************************************************************************************************************************

//Error 1 ) Microsoft.Data.SqlClient.SqlException Message = Error converting data type nvarchar to numeric.

//The data for table-valued parameter "@tblProductsType" doesn't conform to the table type of the parameter. SQL Server error is: 8114, state: 5

//Fix : Always the Order of Columns in User Defined table types should match with the Order of Model Class .cs.
//      Always the Datatype of columns in User Defined table types should match with the Datatypes of Model Class.cs

//========================================================================================================================================

//Error 2 ) Microsoft.Data.SqlClient.SqlException: 'Cannot insert the value NULL into column 'UserId', table 'POS-DEV.dbo.Ms_Category'; column does not allow nulls.
//UPDATE fails.
//Cannot insert the value NULL into column 'UserId', table 'POS-DEV.dbo.Ms_Sub_Category'; column does not allow nulls. UPDATE fails.


//Error 3 ) Microsoft.Data.SqlClient.SqlException: 'Cannot insert the value NULL into column 'UserId', table 'POS-DEV.dbo.Ms_Sub_Category'; column does not allow nulls. UPDATE fails.
//Cannot insert the value NULL into column 'UserId', table 'POS-DEV.dbo.Ms_Products'; column does not allow nulls. UPDATE fails.


//Fix : For the above two error we need to include the UserId column  and values as 1 in insert query of the procedure named [SP_InsertProductOmnitoPos].


//================================================================================================================================