using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Web_Assets_Management.Models;

namespace Web_Assets_Management.DBArea
{
    public class AssetsArea
    {
        public List<Assets> GetAssetsByCategorie(string cName)
        {
            if (cName==null)
            {
                return GetAssets();
            }
            else
            {
                List<Assets> assets = new List<Assets>();
                SqlCommand command = Databases.command("Select * From Assets Where CategorieName=@cName");
                command.Parameters.AddWithValue("@cName", cName);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Assets asset = new Assets(Convert.ToInt32(dataReader[0]), dataReader[1].ToString(), Convert.ToInt32(dataReader[2]), dataReader[3].ToString(), dataReader[4].ToString());
                    assets.Add(asset);
                }
                dataReader.Close();
                return assets;
            }

        }
       

        public bool AddAssets(int number, string name, int price, string categories, string vendor)
        {
            try
            {
                SqlCommand command = Databases.command("Insert Into Assets (AssetsNumber,Name,Price,CategorieName,Vendor) values (@number,@name,@price,@categories,@vendor)");
                command.Parameters.AddWithValue("@number", number);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@categories", categories);
                command.Parameters.AddWithValue("@vendor", vendor);
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

        public bool DeleteAssets (int number)
        {
            try
            {
                SqlCommand command = Databases.command("Delete from Assets Where AssetsNumber=@number");
                command.Parameters.AddWithValue("@number", number);
                if (command.ExecuteNonQuery() > 0)
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
            return false;
        }

        public bool UpdateAssets(int number, string name, int price, string categories, string vendor)
        {
            try
            {
                SqlCommand command = Databases.command("Update Assets Set Name=@name ,Price=@price ,CategorieName=@cName ,Vendor=@vendor Where AssetsNumber=@number");
                command.Parameters.AddWithValue("@number",number);
                command.Parameters.AddWithValue("@name",name);
                command.Parameters.AddWithValue("@price",price);
                command.Parameters.AddWithValue("@cName",categories);
                command.Parameters.AddWithValue("@vendor",vendor);
                if (command.ExecuteNonQuery()>0)
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
        public List<Assets> GetAssets()
        {
            List<Assets> assets = new List<Assets>();
            SqlCommand command = Databases.command("Select * From Assets");
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Assets asset = new Assets(Convert.ToInt32(dataReader[0]), dataReader[1].ToString(), Convert.ToInt32(dataReader[2]), dataReader[3].ToString(), dataReader[4].ToString());
                assets.Add(asset);
            }
            dataReader.Close();
            return assets;
        }
        public List<Assets> SearchByValue(string value)
        {
            List<Assets> assets = new List<Assets>();
            SqlCommand command = Databases.command("SELECT * FROM Assets WHERE AssetsNumber LIKE '%' + @value + '%' OR Name LIKE '%' + @value + '%' OR Price LIKE '%' + @value + '%' OR CategorieName LIKE '%' + @value + '%' OR Vendor LIKE '%' + @value + '%'");
            command.Parameters.AddWithValue("@value", value);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Assets asset = new Assets(Convert.ToInt32(dataReader[0]), dataReader[1].ToString(), Convert.ToInt32(dataReader[2]), dataReader[3].ToString(), dataReader[4].ToString());
                assets.Add(asset);
            }
            dataReader.Close();
            return assets;
        }

        public List<Assets> SearchCategoriesByValue(string value, string CategorieName)
        {
            List<Assets> assets = new List<Assets>();
            SqlCommand command = Databases.command("SELECT * FROM Assets WHERE AssetsNumber LIKE '%' + @value + '%' OR Name LIKE '%' + @value + '%' OR Price LIKE '%' + @value + '%' OR Vendor LIKE '%' + @value + '%' AND CategorieName LIKE '%' + @CategorieName + '%'");
            command.Parameters.AddWithValue("@value", value);
            command.Parameters.AddWithValue("@CategorieName", CategorieName);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Assets asset = new Assets(Convert.ToInt32(dataReader[0]), dataReader[1].ToString(), Convert.ToInt32(dataReader[2]), dataReader[3].ToString(), dataReader[4].ToString());
                assets.Add(asset);
            }
            dataReader.Close();
            return assets;
        }

    }
}
