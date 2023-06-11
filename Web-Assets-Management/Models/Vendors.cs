using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Assets_Management.Models
{
    public class Vendors
    {
        public int VendorId { get; set; }
        public string VendorName{ get; set; }
        public Vendors(int cId,string cName)
        {
            VendorId = cId;
            VendorName= cName;
        }
    }
}
