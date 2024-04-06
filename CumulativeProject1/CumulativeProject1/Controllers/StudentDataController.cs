using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativeProject1.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Resultset;

namespace CumulativeProject1.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the students table of our school database.
        /// <summary>
        /// Returns a list of student in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of Student 
        /// </returns>
        [HttpGet]
        
        public IEnumerable<Students> ListStudents()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Student Names
            List<Students> Student = new List<Students> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string Studentnumber = ResultSet["studentnumber"].ToString();
                string Enroldate = ResultSet["enroldate"].ToString();


                Students Newstudent = new Students();
                Newstudent.studentId = StudentId;
                Newstudent.studentFname = StudentFname;
                Newstudent.studentLname = StudentLname;
                Newstudent.studentnumber = Studentnumber;
                Newstudent.enroldate = Enroldate;

                //Add the Student Name to the List
                Student.Add(Newstudent);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of student names
            return Student;
        }


        /// <summary>
        /// Finds an Student in the system given an ID
        /// </summary>
        /// <param name="id">The student primary key</param>
        /// <returns>An student object</returns>
        [HttpGet]
        [Route("api/StudentData/FindStudent/{id}")]
        public Students FindStudents(int id)
        {
            Students Newstudent = new Students();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Students where studentid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string Studentnumber = ResultSet["studentnumber"].ToString();
                string Enroldate = ResultSet["enroldate"].ToString();


                //Students Newstudent = new Students();
                Newstudent.studentId = StudentId;
                Newstudent.studentFname = StudentFname;
                Newstudent.studentLname = StudentLname;
                Newstudent.studentnumber = Studentnumber;
                Newstudent.enroldate = Enroldate;

            }


            return Newstudent;
        }

    }
}
