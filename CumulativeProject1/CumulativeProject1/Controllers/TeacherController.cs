using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProject1.Models;
using Mysqlx.Datatypes;

namespace CumulativeProject1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //GET : /Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teacher = controller.ListTeachers();
            return View(Teacher);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
        //GET : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        //GET : /Teacher/New/
        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
       
        public ActionResult Create(string teacherFname, string teacherLname, string employeenumber, string hiredate , decimal salary)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(teacherFname);
            Debug.WriteLine(teacherLname);
            Debug.WriteLine(employeenumber);

            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherFname = teacherFname;
            NewTeacher.teacherLname = teacherLname;
            NewTeacher.employeenumber = employeenumber;
            NewTeacher.hiredate = hiredate;
            NewTeacher.salary = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        //GET : /Teacher/Modify/{id}
        public ActionResult Modify(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher TeacherInfo = controller.FindTeacher(id);

            return View(TeacherInfo);
        }

        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Author Show" page of our updated author.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="teacherFname">The updated first name of the teacher</param>
        /// <param name="teacherLname">The updated last name of the teacher</param>
        /// <param name="employeenumber">The updated employeenumber of the teacher.</param>
        /// <param name="hiredate">The updated hiredate of the teacher.</param>
        /// <param name="salary">The updated salary of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Author/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"teacherFname":
        ///	"teacherLname":
        ///	"employeenumber":
        ///	"hiredate":
        ///	"salary"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string teacherFname, string teacherLname, string employeenumber, string hiredate, decimal salary)
        {

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.teacherId = id;
            TeacherInfo.teacherFname = teacherFname;
            TeacherInfo.teacherLname = teacherLname;
            TeacherInfo.employeenumber = employeenumber;
            TeacherInfo.hiredate = hiredate;
            TeacherInfo.salary = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(TeacherInfo);

            return RedirectToAction("Show/" + id);
            
        }

    }
} 