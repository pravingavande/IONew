using IOSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace IOSystem.Controllers
{
    public class OrgSectionController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public OrgSectionController(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            //ViewBag.RTypeList = new List<SelectListItem>
            //{
            //    new SelectListItem {Text = "IR", Value = "1"},
            //    new SelectListItem {Text = "OR", Value = "2"}
            //};

            Int64 OrgID = Convert.ToInt64(HttpContext.Session.GetString("OrgID"));
            ViewBag.MaxSrNo = _dbcontext.OrgSectionMaster.Where(s => s.DeleteFlag == "N" && s.OrgID == OrgID).Select(s => s.SrNo).DefaultIfEmpty().ToList().Max() + 1;
            //ViewBag.MaxSrNo = _dbcontext.OrgSectionMaster.Where(s => s.DeleteFlag == "N" && s.OrgID == OrgID).Max(p => p.SrNo) + 1;
            //ViewBag.RTypeList = new SelectList(GetRTypeList(), nameof(RType.RTypeID), nameof(RType.RTypeName));
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrgSection model)
        {
            if (ModelState.IsValid)
            {
                model.OrgID = Convert.ToInt32(HttpContext.Session.GetString("OrgID"));
                model.DeleteFlag = "N";
                _dbcontext.Add(model);

                _dbcontext.SaveChanges();
                TempData["success"] = "Record Saved Sucessfully";
                return RedirectToAction("Index");
            }
            return NotFound();
        }


        public IActionResult Edit(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var data = _dbcontext.OrgSectionMaster.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            //ViewBag.RTypeList = new SelectList(GetRTypeList(), nameof(RType.RTypeID), nameof(RType.RTypeName));

            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(OrgSection model)
        {
            if (ModelState.IsValid)
            {
                model.OrgID = Convert.ToInt32(HttpContext.Session.GetString("OrgID"));
                model.DeleteFlag = "N";
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
                var data = _dbcontext.OrgSectionMaster.Find(id);
                if (data != null)
                {
                    data.DeleteFlag = "Y";
                    _dbcontext.Update(data);
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
                var data = _dbcontext.OrgSectionMaster.OrderBy(sortColumn + " " + sortColumnDirection).AsNoTracking().ToList();

                //get total count of data in table
                totalRecord = data.Count();
                // search data when search value found
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(x =>
                      x.SectionID.ToString() != null && x.SectionID.ToString().ToLower().Contains(searchValue.ToLower())
                      || x.SectionName != null && x.SectionName.ToLower().Contains(searchValue.ToLower())
                    ).ToList();
                }

                // get total count of records after search 
                filterRecord = data.Count();

                ////sort data
                //if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                //    data = data.OrderBy(sortColumn + "" + sortColumnDirection);

                Int64 OrgID = Convert.ToInt64(HttpContext.Session.GetString("OrgID"));
                var recordlist = data.Skip(skip).Take(pageSize).Where(s => s.DeleteFlag == "N" && s.OrgID== OrgID).ToList();

                var returnObj = new { draw = draw, recordsTotal = totalRecord, recordsFiltered = filterRecord, data = recordlist };
                return Json(returnObj);
            }
            else
            {
                return Json(null);
            }
        }    
    }
}
