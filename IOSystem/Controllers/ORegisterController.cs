using IOSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Controllers
{
    public class ORegisterController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public ORegisterController(ApplicationDbContext context)
        {
            _dbcontext = context;
        }
        public IActionResult Index()
        {
            return View();
            //return View(_dbcontext.OutwardRegister.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ORegister model)
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
            var data = _dbcontext.OutwardRegister.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(ORegister model)
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
                var data = _dbcontext.OutwardRegister.Find(id);
                if (data != null)
                {
                    _dbcontext.OutwardRegister.Remove(data);
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
    }
}
