using MVCStudentDetails.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStudentDetails.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        StudentDetailsEntities1 dbobj = new StudentDetailsEntities1();
        public ActionResult Student(tbl_MVCStudent obj)
        {
            return View(obj);
            //if (obj != null)
            //{
            //    return View(obj);
            //}
            //else
            //{
            //    return View();
            //}
        }
        [HttpPost]
        public ActionResult AddStudent(tbl_MVCStudent model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tbl_MVCStudent obj = new tbl_MVCStudent();
                    obj.ID = model.ID;
                    obj.Fname = model.Fname;
                    obj.Lname = model.Lname;
                    obj.Email = model.Email;
                    obj.Mobile = model.Mobile;
                    obj.Description = model.Description;

                    if (model.ID == 0) /*for Insert*/
                    {
                        dbobj.tbl_MVCStudent.Add(obj);
                        dbobj.SaveChanges();
                    }
                    else
                    {
                        dbobj.Entry(obj).State = EntityState.Modified;
                        dbobj.SaveChanges();
                    }
                }
                ModelState.Clear();
                return View("Student");
            }
            catch (Exception ex)
            {
                return View("Student");
            }
        }

        public ActionResult StudentList()
        {
            var res = dbobj.tbl_MVCStudent.ToList();
            return View(res);
        }

        public ActionResult Delete(int ID)
        {
            var res = dbobj.tbl_MVCStudent.Where(x => x.ID == ID).First();
            dbobj.tbl_MVCStudent.Remove(res);
            dbobj.SaveChanges();

            var list = dbobj.tbl_MVCStudent.ToList();

            return View("StudentList", list);
        }
    }
}
