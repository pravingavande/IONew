using IOSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace IOSystem.Controllers
{
    public class IRegisterController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public IRegisterController(ApplicationDbContext context)
        {
            _dbcontext = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            Int64 OrgID = Convert.ToInt64(HttpContext.Session.GetString("OrgID"));

            ViewBag.LetterCategoryList = _dbcontext.OrgLetterCategoryMaster.Where(s => s.OrgID == OrgID && s.DeleteFlag == "N").OrderBy(x => x.SrNo).ToList();
            ViewBag.RegisterList = _dbcontext.OrgRegisterMaster.Where(s => s.OrgID == OrgID && s.DeleteFlag == "N").OrderBy(x => x.SrNo).ToList();
            ViewBag.FYMasterList = _dbcontext.OrgFYMaster.Where(s => s.OrgID == OrgID && s.DeleteFlag == "N").OrderByDescending(x => x.FYID).ToList();

            var data= (from p in _dbcontext.OrgEmpSectionDefine
                       join e in _dbcontext.OrgEmpMaster on p.EmpID equals e.EmpID
                       join q in _dbcontext.OrgSectionMaster on p.SectionID equals q.SectionID
                       select new OrgEmpSection
                       {
                           ESDID = p.ESDID,
                           EmpID = p.EmpID,
                           SectionID = p.SectionID,
                           EmpSectionName = q.SectionName + '-' + e.EmpName
                       }).ToList();

            ViewBag.EmpSectionNameList = data;

            //ViewBag.EmpNameList = this._dbcontext.EmpSectionList("EmpSection").FirstOrDefault(); 
            ////var param1Value = new SqlParameter("RecordSelection", "EmpSection");
            //SqlParameter RecordSelection = new SqlParameter
            //{
            //    ParameterName = "RecordSelection",
            //    SqlDbType = SqlDbType.NVarChar,
            //    Value = "EmpSection",
            //    Direction = ParameterDirection.Input,
            //    Size = 50
            //};

            //ViewBag.EmpNameList = this._dbcontext.EmpSectionMaster.FromSqlRaw("EXECUTE [dbo].[SP_FillList] @Param1", RecordSelection).ToList();
            //var RecordSelection = "EmpSection";
            //var blogs = _dbcontext.EmpSectionMaster.FromSqlRaw("EXECUTE dbo.SP_FillList {0}", RecordSelection).ToList();
            ViewBag.IRMaxNo = _dbcontext.InwardRegister.Select(t => t.IRNo).DefaultIfEmpty().ToList().Max()+1;

            //ViewBag.IRMaxNo = _dbcontext.InwardRegister.Max(p => Convert.ToString(p.IRNo) == null ? 1 : p.IRNo + 1).single;
            ViewBag.IRInputType = "M";

            return View();
        }

        public IActionResult GetMaxNo(Int64 OrgFYID, Int64 OrgRegisterCode)
        {
            ViewBag.IRMaxNo = _dbcontext.InwardRegister.Where(s => s.FYID == OrgFYID && s.RegisterID == OrgRegisterCode).Max(p => p.IRNo) + 1;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IRegister model)
        {
            if (ModelState.IsValid)
            {
                model.OrgID = Convert.ToInt32(HttpContext.Session.GetString("OrgID"));
                var items = _dbcontext.OrgEmpSectionDefine.Where(x => x.ESDID == model.ESDID)
                  .Select(x => new
                  {
                      EmpID = x.EmpID,
                      SectionID = x.SectionID
                  }).FirstOrDefault();

                model.EmpID = Convert.ToInt64(items.EmpID);
                model.SectionID = Convert.ToInt64(items.SectionID);

                //ViewBag.SectionID = _dbcontext.OrgEmpSectionDefine.Where(i => i.EmpSectionName == "abc").ToList();
                //ViewBag.EmpID = _dbcontext.OrgEmpSectionDefine.Where(i => i.EmpSectionName == "abc").ToList();
                _dbcontext.Add(model);
                _dbcontext.SaveChanges();

                TempData["success"] = "Record Saved Sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.OrgLetterCategoryList = _dbcontext.OrgLetterCategoryMaster.ToList();
            var data = _dbcontext.InwardRegister.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(IRegister model)
        {
            if (ModelState.IsValid)
            {
                model.OrgID = Convert.ToInt32(HttpContext.Session.GetString("OrgID"));

                _dbcontext.Update(model);
                _dbcontext.SaveChanges();
                TempData["success"] = "Record Edit Sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(Int64? id)
        {
            if (id != null)
            {
                var data = _dbcontext.InwardRegister.Find(id);
                if (data != null)
                {
                    _dbcontext.InwardRegister.Remove(data);
                    _dbcontext.SaveChanges();

                    TempData["warning"] = "Record Delete Sucessfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public JsonResult DisplayRecord()
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            if (!string.IsNullOrEmpty(sortColumn))
            {
                // Getting all Customer data
                var data = (from InwardRegister in _dbcontext.InwardRegister
                            select InwardRegister).AsNoTracking().ToList();

                //// Getting all Customer data
                //var data = (from InwardRegister in _dbcontext.InwardRegister
                //                    select InwardRegister).OrderBy(sortColumn + " " + sortColumnDirection).AsNoTracking().ToList(); ;

                //var data = _dbcontext.InwardRegister.OrderBy(sortColumn + " " + sortColumnDirection).AsNoTracking().ToList();
                //var data = _dbcontext.InwardRegister.ToList();

                //var data =(from IRegister in _dbcontext.InwardRegister 
                //            select new
                //            {
                //                IRID = IRegister.IRID
                //              }
                //          ).ToList();


                //get total count of data in table
                totalRecord = data.Count();
                // search data when search value found
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(x =>
                      x.SubjectName != null && x.SubjectName.ToLower().Contains(searchValue.ToLower())
                      || x.SchoolName != null && x.SchoolName.ToLower().Contains(searchValue.ToLower())
                      || x.IRID.ToString().ToLower().Contains(searchValue.ToLower())
                    ).ToList();
                }

                // get total count of records after search 
                filterRecord = data.Count();

                ////sort data
                //if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                //    data = data.OrderBy(sortColumn + "" + sortColumnDirection);

                Int64 OrgID = Convert.ToInt64(HttpContext.Session.GetString("OrgID"));
                var empList = data.Skip(skip).Take(pageSize).Where(s => s.OrgID == OrgID).ToList();

                var returnObj = new { draw = draw, recordsTotal = totalRecord, recordsFiltered = filterRecord, data = empList };
                return Json(returnObj);
            }
            else
            {
                return Json(null);
            }
        }

        public JsonResult CheckMaxNoJSON(Int64 OrgFYID, Int64 OrgRegisterCode)
        {
            try
            {
                var result = _dbcontext.InwardRegister.Where(s => s.FYID == OrgFYID && s.RegisterID == OrgRegisterCode).Max(p => p.IRNo) + 1;
                return Json(data: result);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
