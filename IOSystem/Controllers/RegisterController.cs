using IOSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IOSystem.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public RegisterController(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Register = _dbcontext.OrgLetterCategoryMaster.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Register model)
        {
            if (ModelState.IsValid)
            {
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
            var data = _dbcontext.OrgRegisterMaster.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Register model)
        {
            if (ModelState.IsValid)
            {
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
                var data = _dbcontext.OrgRegisterMaster.Find(id);
                if (data != null)
                {
                    data.DeleteFlag = "Y";
                    _dbcontext.Update(data);
                    _dbcontext.SaveChanges();
                    //TempData["success"] = "Record Edit Sucessfully";
                    //return RedirectToAction("Index");

                    //_dbcontext.RegisterMaster.Remove(data);
                    //_dbcontext.SaveChanges();

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
                var data = _dbcontext.OrgRegisterMaster.OrderBy(sortColumn + " " + sortColumnDirection).AsNoTracking().ToList();

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
                      x.RegisterID.ToString() != null && x.RegisterID.ToString().ToLower().Contains(searchValue.ToLower())
                      || x.RegisterName != null && x.RegisterName.ToLower().Contains(searchValue.ToLower())
                    ).ToList();
                }

                // get total count of records after search 
                filterRecord = data.Count();

                ////sort data
                //if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                //    data = data.OrderBy(sortColumn + "" + sortColumnDirection);

                var empList = data.Skip(skip).Take(pageSize).Where(s => s.DeleteFlag == "N").ToList();

                var returnObj = new { draw = draw, recordsTotal = totalRecord, recordsFiltered = filterRecord, data = empList };
                return Json(returnObj);
            }
            else
            {
                return Json(null);
            }
        }

    }
}
