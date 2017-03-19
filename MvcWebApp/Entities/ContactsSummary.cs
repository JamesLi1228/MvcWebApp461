using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcWebApp.Entities
{
    public class ContactsSummary
    {
        public ContactsSummary()
        {
            AddresNumbers = new List<AddressesPerContact>();
        }

        [Display(Name="Number Of Contacts")]
        public int NumberOfContacts { get; set; }

        [Display(Name = "Total Of Computers")]
        public int TotalOfComputers { get; set; }

        public virtual ICollection<AddressesPerContact> AddresNumbers { get; set; }
    }
}