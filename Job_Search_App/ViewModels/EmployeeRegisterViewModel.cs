using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Search_App.ViewModels

{
    public class EmployeeRegisterViewModel
    {
        [MaxLength(60), Display(Name = "User Name", Prompt = "User Name")]
        public string UserName { get; set; }

        [Required, MaxLength(20), Display(Name = "First Name", Prompt = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(60), Display(Name = "Last Name", Prompt = "Last Name")]
        public string LastName { get; set; }

        [Required, MaxLength(60), Display(Name = "Email", Prompt = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password", Prompt = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password)), Display(Name = "Confirm Password", Prompt = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [  Display(Name = "Address", Prompt = "Address")]
        public string Streetaddress { get; set; }
        [ Display(Name = "City", Prompt = "City")]
        public string City { get; set; }

        [DataType(DataType.PostalCode),  Display(Name = "ZipCode", Prompt = "ZipCode")]
        public string Zipcode { get; set; }

        [ Display(Name = "Country", Prompt = "Country")]
        public string Country { get; set; }
        [DataType(DataType.DateTime), Display(Name = "Date of Birth", Prompt = "Date of Birth")]
        public DateTime Birthday { get; set; }

        [DataType(DataType.PhoneNumber),  Display(Name = "Phone", Prompt = "Phone")]
        public string Telephonenumber { get; set; }
    }
}
