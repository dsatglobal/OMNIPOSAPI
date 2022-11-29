using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System;

namespace POS_API.Models.Customer
{
    public class DALCustomer
    {
        public string connectionstring { get; set; }

        public DALCustomer()
        {
            connectionstring = GetConString.ConString();
        }

        //For ListToDataTableConveter:

        ListtoDataTableConverter listdata = new ListtoDataTableConverter();

        public List<Customer> GetCustomerOmnitoPOS()
        {
            List<Customer> customerList = new List<Customer>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("USP_GetCustomerOmnitoPOS", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer obj_customer = new Customer();
                    obj_customer.CustomerName = rdr["CustomerName"].ToString();
                    obj_customer.CustomerName_AR = rdr["CustomerName_AR"].ToString();
                    obj_customer.ContactName = rdr["ContactPerson"].ToString();
                    obj_customer.Phone = rdr["Phone"].ToString();
                    obj_customer.Email = rdr["Email"].ToString();
                    obj_customer.Mobile = rdr["Mobile"].ToString();
                    obj_customer.Address = rdr["Address"].ToString();
                    obj_customer.POBox = rdr["PO_Box"].ToString();
                    obj_customer.ZipCode = rdr["ZipCode"].ToString();
                    obj_customer.CustomerGenerateNo = rdr["CustomerGenerateNo"].ToString();
                    obj_customer.StoreName = rdr["StoreName"].ToString();
                    obj_customer.MerchantName = rdr["MerchantName"].ToString();
                    obj_customer.City = rdr["CityName"].ToString();
                    obj_customer.Country = rdr["CountryName"].ToString();
                    obj_customer.Discounts = rdr["Discounts"].ToString();
                    customerList.Add(obj_customer);
                }
                con.Close();
            }
            return customerList;
        }

        public void AddCustomerOmniToPos(List<Customer> customerList)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertCustomerOmnitoPos", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                DataTable dt1 = new DataTable();
                dt1 = listdata.ToDataTable<Customer>(customerList);

                cmd.Parameters.AddWithValue("@tblcustomerType", dt1);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
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
}
