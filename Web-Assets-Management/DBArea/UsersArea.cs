using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Web_Assets_Management.Models;

namespace Web_Assets_Management.DBArea
{
    public class UsersArea
    {
        public List<Users> GetUsers()
        {
            List<Users> users = new List<Users>();
            SqlCommand command = Databases.command("Select * From Users");
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Users user = new Users(Convert.ToInt32(dataReader[0]),dataReader[1].ToString(),dataReader[2].ToString());
                users.Add(user);
            }
            return users;
        }

        public bool UsersAuth(int id,string pass)
        {
            SqlCommand command = Databases.command("Select COUNT(*) From Users WHERE EId=@EId and Password=@pass");
            command.Parameters.AddWithValue("@EId", id);
            command.Parameters.AddWithValue("@pass", pass);
            bool result = Convert.ToBoolean(command.ExecuteScalar());
            return result;
        }

        public List<Users> GetUsersAuth(int eid)
        {
            List<Users> users = new List<Users>();
            SqlCommand command = Databases.command("Select * From Users Where EId=@EId");
            command.Parameters.AddWithValue("@EId",eid);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Users user = new Users(Convert.ToInt32(dataReader[0]),dataReader[1].ToString(),dataReader[2].ToString());
                users.Add(user);
            }
            dataReader.Close();
            return users;
        }
    }
}
