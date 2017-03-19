using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWebApp.Entities
{
    public class Address
    {
        public int ID { get; set; }
        public int ContactID { get; set; }
        public virtual Contact Contact { get; set; }
        public string Address1 { get; set; }
        public string Addres2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Zip { get; set; }
    }
}