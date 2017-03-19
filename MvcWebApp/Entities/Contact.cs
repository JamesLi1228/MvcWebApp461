using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWebApp.Entities
{
    public class Contact
    {
        public Contact()
        {
            addresses = new List<Address>();
        }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string BirthDate { get; set; }
        public int NumberOfComputers { get; set; }
        public virtual ICollection<Address> addresses { get; set; }
    }
}