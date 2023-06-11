using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Web_Assets_Management.Models;

namespace Web_Assets_Management.DBArea
{
    public class VendorsArea
    {

        public List<Vendors> GetVendors()
        {
            List<Vendors> vendors = new List<Vendors>();
            SqlCommand command = Databases.command("Select * From Vendors");
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Vendors vendor = new Vendors(Convert.ToInt32(dataReader[0]),dataReader[1].ToString());
                vendors.Add(vendor);
            }
            dataReader.Close();
            return vendors;
        }
    }
}
