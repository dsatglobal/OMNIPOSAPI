using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace POS_API.Models.POSUser
{
    public class DALPOSUser
    {
        public string connectionstring { get; set; }

        public DALPOSUser()
        {
            connectionstring = GetConString.ConString();
        }

        //For ListToDataTableConveter:

        ListtoDataTableConverter listdata = new ListtoDataTableConverter();

        public List<POSUser> GetPOSUserlistOmnitoPOS()
        {
            List<POSUser> posuserList = new List<POSUser>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("SP_GetPOSListOmnitoPosAPI", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    POSUser obj_posuser = new POSUser();
                    obj_posuser.UserName = rdr["UserName"].ToString();
                    obj_posuser.Password = rdr["Password"].ToString();
                    obj_posuser.MerchantName = rdr["MerchantName"].ToString();
                    obj_posuser.MContactName = rdr["MContactName"].ToString();
                    obj_posuser.MAddress = rdr["MAddress"].ToString();
                    obj_posuser.MPhone = rdr["MPhone"].ToString();
                    obj_posuser.MEmail = rdr["MEMail"].ToString();
                    obj_posuser.MMobile = rdr["MMobile"].ToString();
                    obj_posuser.MCity = rdr["MCity"].ToString();
                    obj_posuser.MCountry = rdr["MCountry"].ToString();
                    obj_posuser.StoreName = rdr["StoreName"].ToString();
                    obj_posuser.SContactName = rdr["SContactName"].ToString();
                    obj_posuser.SAddress = rdr["SAddress"].ToString();
                    obj_posuser.SContactNumber = rdr["SContactNumber"].ToString();
                    obj_posuser.SEmail = rdr["SEMail"].ToString();
                    obj_posuser.SCity = rdr["SCity"].ToString();
                    obj_posuser.SCountry = rdr["SCountry"].ToString();
                    posuserList.Add(obj_posuser);
                }
                con.Close();
            }
            return posuserList;
        }

        public void AddUserOmniToPos(List<POSUser> posuserList)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertPOSUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                DataTable dt1 = new DataTable();
                dt1 = listdata.ToDataTable<POSUser>(posuserList);

                cmd.Parameters.AddWithValue("@tblPosuserType", dt1);
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
