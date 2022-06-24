using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Class> ClassMaster { get; set; }
        public DbSet<Category> CategoryMaster { get; set; }
        public DbSet<OrgSection> OrgSectionMaster { get; set; }
        public DbSet<OrgDesignation> OrgDesignationMaster { get; set; }
        public DbSet<IRegister> InwardRegister { get; set; }
        public DbSet<ORegister> OutwardRegister { get; set; }
        public DbSet<OrgRegister> OrgRegisterMaster { get; set; }
        public DbSet<OrgLetterCategory> OrgLetterCategoryMaster { get; set; }
        public DbSet<OrgFY> OrgFYMaster { get; set; }
        public DbSet<OrgEmpSection> OrgEmpSectionDefine { get; set; }
        public DbSet<OrgEmp> OrgEmpMaster { get; set; }
        public DbSet<OrgEmpType> OrgEmpTypeMaster { get; set; }


        //public DbSet<OrgEmpSectionDefine> EmpSectionDefine { get; set; }
        //public DbSet<EmpSection> EmpSectionMaster { get; set; }

        //public static DataTable ListToDataTable<T>(IList<T> data)
        //{
        //    DataTable table = new DataTable();

        //    IConfigurationBuilder builder = new ConfigurationBuilder();
        //    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
        //    var root = builder.Build();
        //    var ConnectionString = root.GetConnectionString("DefaultConnection");

        //    var dt = new DataTable("EmpSectionName");
        //    using (SqlConnection con = new SqlConnection(ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_FillList", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@RecordSelection", "EmpSection");
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    con.Close();
        //                }
        //            }
        //        }
        //    }
        //    List<OrgEmpSectionDefine> EmployeeList = new List<OrgEmpSectionDefine>();
        //    EmployeeList = (from DataRow dr in dt.Rows select new OrgEmpSectionDefine()
        //                    {
        //                        ESDID = Convert.ToInt32(dr["ESDID"]),
        //                        EmpID = Convert.ToInt32(dr["EmpID"]),
        //                        EmpSectionName = dr["EmpSectionName"].ToString(),
        //                        SectionID = Convert.ToInt32(dr["SectionID"])
        //                    }).ToList();

        //    return EmployeeList();

        //    //return this.EmpSectionMaster.("[dbo].[SP_FillList] @RecordSelection", pRecordSelection);
        //    //return this.EmpSectionMaster.FromSqlRaw("EXEC [dbo].[SP_FillList] @RecordSelection", RecordSelection);
        //    //return this.EmpSectionMaster.FromSqlRaw("EXECUTE [dbo].[SP_FillList] @RecordSelection", RecordSelection);
        //}

        //public IQueryable<EmpSection> EmpSectionList(string recordSelection)
        //{
        //    IConfigurationBuilder builder = new ConfigurationBuilder();
        //    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
        //    var root = builder.Build();
        //    var ConnectionString = root.GetConnectionString("DefaultConnection");

        //    var dt = new DataTable();
        //    using (SqlConnection con = new SqlConnection(ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_GetLogin", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@UserName", login.EmpUserName);
        //        cmd.Parameters.AddWithValue("@Password", login.EmpPassword);
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    HttpContext.Session.SetString("EmpID", row["EmpID"].ToString());
        //                    HttpContext.Session.SetString("EmpTypeID", row["EmpTypeID"].ToString());
        //                    HttpContext.Session.SetString("OrgID", row["OrgID"].ToString());
        //                    HttpContext.Session.SetString("EmpName", row["EmpName"].ToString());
        //                    HttpContext.Session.SetString("OrgNameShort", row["OrgNameShort"].ToString());
        //                    con.Close();

        //                    return RedirectToAction("Index", "Home");
        //                }
        //            }
        //        }
        //    }


        //    SqlParameter pRecordSelection = new SqlParameter("@RecordSelection", recordSelection);
        //    SqlParameter RecordSelection = new SqlParameter
        //    {
        //        ParameterName = "RecordSelection",
        //        SqlDbType = SqlDbType.NVarChar,
        //        Value = "EmpSection",
        //        Direction = ParameterDirection.Input,
        //        Size = 50
        //    };

        //    //return this.EmpSectionMaster.("[dbo].[SP_FillList] @RecordSelection", pRecordSelection);
        //    return this.EmpSectionMaster.FromSqlRaw("EXEC [dbo].[SP_FillList] @RecordSelection", RecordSelection);
        //    //return this.EmpSectionMaster.FromSqlRaw("EXECUTE [dbo].[SP_FillList] @RecordSelection", RecordSelection);
        //}


    }
}
