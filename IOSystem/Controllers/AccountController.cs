using IOSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            try
            {
                if (!string.IsNullOrEmpty(login.EmpUserName) && !string.IsNullOrEmpty(login.EmpPassword))
                {
                    IConfigurationBuilder builder = new ConfigurationBuilder();
                    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                    var root = builder.Build();
                    var ConnectionString = root.GetConnectionString("DefaultConnection");

                    var dt = new DataTable();
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_GetLogin", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", login.EmpUserName);
                        cmd.Parameters.AddWithValue("@Password", login.EmpPassword);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    HttpContext.Session.SetString("EmpID", row["EmpID"].ToString());
                                    HttpContext.Session.SetString("EmpTypeID", row["EmpTypeID"].ToString());
                                    HttpContext.Session.SetString("OrgID", row["OrgID"].ToString());
                                    HttpContext.Session.SetString("EmpName", row["EmpName"].ToString());
                                    HttpContext.Session.SetString("OrgNameShort", row["OrgNameShort"].ToString());
                                    con.Close();

                                    return RedirectToAction("Index", "Home");
                                }
                            }
                        }
                    }

                    TempData["msg"] = "UserName Or Password Is Wrong.!";
                    return View();
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Forgot()
        {
            return View();
        }

    }
}
