using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Assets_Management.Models
{
    public class Reports
    {
        public DateTime Date { get; set; }
        public int EId { get; set; }
        public string Auth { get; set; }
        public string Information { get; set; }
        public string Url { get; set; }
        public Reports(DateTime date,int eId,string auth,string inf,string url)
        {
            Date = date;
            EId = eId;
            Auth = auth;
            Information = inf;
            Url = url;
        }
    }
}
