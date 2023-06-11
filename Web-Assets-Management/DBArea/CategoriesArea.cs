using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Web_Assets_Management.Models;

namespace Web_Assets_Management.DBArea
{
    public class CategoriesArea
    {
        public List<Categories> GetCategories()
        {
            List<Categories> categories = new List<Categories>();
            SqlCommand command = Databases.command("Select * from Categories");
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Categories categorie = new Categories(Convert.ToInt32(dataReader[0]), dataReader[1].ToString());
                categories.Add(categorie);
            }
            dataReader.Close();
            return categories;
        }

        public bool AddCategories(string cName)
        {
            try
            {
                SqlCommand command1 = Databases.command("Select COUNT(*) From Categories Where CategorieName=@cName");
                command1.Parameters.AddWithValue("@cName",cName);
                int exist = (int)command1.ExecuteScalar();
                if (exist==0)
                {
                    SqlCommand command = Databases.command("Insert Into Categories (CategorieName) values (@cName)");
                    command.Parameters.AddWithValue("@cName", cName);
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
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
