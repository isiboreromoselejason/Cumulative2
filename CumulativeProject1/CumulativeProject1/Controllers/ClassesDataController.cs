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
    public class ClassesDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Classes table of our school database.
        /// <summary>
        /// Returns a list of Classes in the system
        /// </summary>
        /// <example>GET api/ClassesData/ListClasses</example>
        /// <returns>
        /// A list of Classes 
        /// </returns>
        [HttpGet]
        public IEnumerable<Classes> ListClasses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Classes Names
            List<Classes> Classes = new List<Classes> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string Classcode = ResultSet["classcode"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string Startdate = ResultSet["startdate"].ToString();
                string Finishdate = ResultSet["finishdate"].ToString();
                string Classname = ResultSet["classname"].ToString();


                Classes Newclasses = new Classes();
                Newclasses.classId = ClassId;
                Newclasses.Classcode = Classcode;
                Newclasses.teacherId = TeacherId;
                Newclasses.startdate = Startdate;
                Newclasses.finishdate = Finishdate;
                Newclasses.Classname = Classname;

                //Add the Classes Name to the List
                Classes.Add(Newclasses);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of classes names
            return Classes;
        }


        /// <summary>
        /// Finds the class in the system given an ID
        /// </summary>
        /// <param name="id">The class primary key</param>
        /// <returns>An class object</returns>
        [HttpGet]
        [Route("api/ClassesData/FindClasses/{id}")]
        public Classes FindClasses(int id)
        {
            Classes NewClasses = new Classes();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where classid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string Classcode = ResultSet["classcode"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string Startdate = ResultSet["startdate"].ToString();
                string Finishdate = ResultSet["finishdate"].ToString();
                string Classname = ResultSet["classname"].ToString();


                Classes Newclasses = new Classes();
                Newclasses.classId = ClassId;
                Newclasses.Classcode = Classcode;
                Newclasses.teacherId = TeacherId;
                Newclasses.startdate = Startdate;
                Newclasses.finishdate = Finishdate;
                Newclasses.Classname = Classname;
            }


            return NewClasses;
        }

    }
}

