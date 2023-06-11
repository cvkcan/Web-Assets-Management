using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Web_Assets_Management.Models;

namespace Web_Assets_Management.DBArea
{
    public class ReportsArea
    {
        public List<Reports> GetReports()
        {
            List<Reports> reports= new List<Reports>();
            SqlCommand command = Databases.command("Select * From Reports");
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Reports report = new Reports(Convert.ToDateTime(dataReader[0]),Convert.ToInt32(dataReader[1]),dataReader[2].ToString(),dataReader[3].ToString(),dataReader[4].ToString());
                reports.Add(report);
            }
            dataReader.Close();
            return reports;
        }
        public bool AddReports(DateTime date, int eId, string auth,string url)
        {
            try
            {
                SqlCommand command = Databases.command("Insert Into Reports (Date,EId,Auth,Information,Url) values (@date,@eId,@auth,@inf,@url)");
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@eId", eId);
                command.Parameters.AddWithValue("@auth", auth);
                command.Parameters.AddWithValue("@inf", $"Unauthorized access was detected by {eId} at {date}. Authorization level is {auth}. ");
                command.Parameters.AddWithValue("@url",url);
                if (command.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());

                return false;
            }
            return false;
        }
    }
}
