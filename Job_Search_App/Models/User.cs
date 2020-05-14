using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Search_App.Models
{
    public class User :IdentityUser
    {

        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(60)]
        public string LastName { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }

        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }

        public DateTime Birthday { get; set; }

        public string Telephonenumber { get; set; }
    }
}
