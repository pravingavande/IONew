using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using IOSystem.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace IOSystem.Models
{
    public class SPCommon
    {
        static public int LoginCheck(Login login)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var ConnectionString = root.GetConnectionString("DefaultConnection");

            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-07AD3O7;Initial Catalog=IOSystem;Integrated Security=True");
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand com = new SqlCommand("SP_Login", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserName", login.EmpUserName);
            com.Parameters.AddWithValue("@Password", login.EmpPassword);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@Isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            int res = Convert.ToInt32(oblogin.Value);
            con.Close();
            return res;
        }
    }
}
