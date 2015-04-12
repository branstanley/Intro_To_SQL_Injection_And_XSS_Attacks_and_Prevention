// <copyright file="DatabaseAccess.cs" company="engi">
// XSS Attack Testing Class.
// </copyright>

namespace Security.App_Code
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using Npgsql;
    using NpgsqlTypes;

    /// <summary>
    /// This class provides all Database Access methods for the project.
    /// </summary>
    public static class DatabaseAccess
    {
        /// <summary>
        /// Creates Database input using a string concatenation
        /// </summary>
        /// <param name="name">the Student name entered in the DB</param>
        /// <param name="gpa">the Student gpa entered into the database</param>
        public static void CreateDataUnsafe(string name, string gpa)
        {
            using (NpgsqlConnection connection = DatabaseConnection())
            using (NpgsqlCommand command = new NpgsqlCommand("insert into students (name, gpa) values (\'" + name + "\', " + gpa + ");", connection))
            {
                connection.Open();
                command.ExecuteReader();
            }
        }

        /// <summary>
        /// Creates Database input using a prepared SQL statement
        /// </summary>
        /// <param name="name">the Student name entered in the DB</param>
        /// <param name="gpa">the Student gpa entered into the database</param>
        public static void CreateDataSafe(string name, string gpa)
        {
            using (NpgsqlConnection connection = DatabaseConnection())
			using (NpgsqlCommand command = new NpgsqlCommand("insert into students (name, gpa) values ( :pName, :pGpa);", connection))
			{
				connection.Open();

				command.Parameters.Add(new NpgsqlParameter("pName", NpgsqlDbType.Text) { Value = name });
				command.Parameters.Add(new NpgsqlParameter("pGpa", NpgsqlDbType.Real) { Value = float.Parse(gpa) });

				command.ExecuteReader();
            }
            
        }

        /// <summary>
        /// Reads all rows from the database, and returns the results after doing HTML Encoding.
        /// </summary>
        /// <returns>A list of all students in the database.</returns>
        public static List<Student> ReadDataSafe()
        {
            using ( NpgsqlConnection connection = DatabaseConnection() )
            using ( NpgsqlCommand command = new NpgsqlCommand("select ID, Name, GPA from Students;", connection) )
            {
                connection.Open();
				using (NpgsqlDataReader results = command.ExecuteReader())
				{
                List<Student> students = new List<Student>();
                foreach (IDataRecord temp in results)
                {
						students.Add(new Student((int)temp["ID"], HttpUtility.HtmlEncode((string)temp["Name"]), (double)temp["GPA"]));
                }

                return students;
            }
        }
        }

        /// <summary>
        /// Reads all rows from the database, and returns the results without encoding
        /// </summary>
        /// <returns>List of all Students in the database.</returns>
        public static List<Student> ReadDataUnsafe()
        {
            using (NpgsqlConnection connection = DatabaseConnection())
            using (NpgsqlCommand command = new NpgsqlCommand("select ID, Name, GPA from Students;", connection))
			{
				connection.Open();
				using (NpgsqlDataReader results = command.ExecuteReader())
				{
					List<Student> students = new List<Student>();
					foreach (IDataRecord temp in results)
					{
							students.Add(new Student((int)temp["ID"], (string)temp["Name"], (double)temp["GPA"]));
					}

					return students;

				}
			}
        }

        /// <summary>
        /// This is the method that provides a connection to Postgres Database.
        /// </summary>
        /// <returns>A connection to the database.</returns>
        private static NpgsqlConnection DatabaseConnection()
        {
            NpgsqlConnectionStringBuilder myBuilder = new NpgsqlConnectionStringBuilder();
            myBuilder.Host = "127.0.0.1";
            myBuilder.Port = 5432;
            myBuilder.Database = "Assignment_4";
            myBuilder.IntegratedSecurity = true;
            return new NpgsqlConnection(myBuilder);
        }
    }
}