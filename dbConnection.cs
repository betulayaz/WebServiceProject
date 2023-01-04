using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebServiceProject
{
    public class dbConnection
    {
        private SqlConnection con;
        private void connection()
        {
            string cnnStr = System.Configuration.ConfigurationManager.AppSettings["db"];
            con = new SqlConnection(cnnStr);

        }
        public DataTable InsertCountry(string country_id, string country_name, string country_capital_city, string country_code, string country_currency, DateTime created_datetime)
        {
            connection();
            SqlCommand com = new SqlCommand("sp_InsertCountry", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@country_id", country_id);
            com.Parameters.AddWithValue("@country_name", country_name);
            com.Parameters.AddWithValue("@country_capital_city", country_capital_city);
            com.Parameters.AddWithValue("@country_code", country_code);
            com.Parameters.AddWithValue("@country_currency", country_currency);
            com.Parameters.AddWithValue("@created_datetime", created_datetime);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            return dt;
        }
    }
}