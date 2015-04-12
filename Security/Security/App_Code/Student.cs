// <copyright file="Student.cs" company="engi">
// Student class, contains the information from the database for each student.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Security.App_Code
{
    /// <summary>
    /// This is the student class, containing a student's ID, Name, and GPA
    /// </summary>
    public class Student
    {
        private int ID;
        private string Name;
        private double GPA;

        public int id
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
            }
        }

        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
        public double gpa
        {
            get
            {
                return GPA;
            }
            set
            {
                GPA = value;
            }
        }

        /// <summary>
        /// Constructor for a student object.
        /// </summary>
        /// <param name="ID">The Student's ID</param>
        /// <param name="Name">The Student's Name</param>
        /// <param name="GPA">The Student's GPA</param>
        public Student(int ID, string Name, double GPA)
        {
            this.ID = ID;
            this.Name = Name;
            this.GPA = GPA;
        }
    }
}