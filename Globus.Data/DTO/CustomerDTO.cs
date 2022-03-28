using Globus.Data.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globus.Data.DTO
{
    public class CustomerDTO : BaseViewModel
    {
       
        public long CustomerId { get; set; }

        [Required(ErrorMessage = "Phone number is rquired")]
        [StringLength(11, ErrorMessage = "The number must be 11 digit", MinimumLength = 11)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is rquired")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is rquired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "State is rquired")]
        public string State { get; set; }

        [Required(ErrorMessage = "LGA is rquired")]
        public string LGA { get; set; }

        public string OTP { get; set; }
        public bool OtpVerified { get; set; }


    }

    public class CustomerDTo
    {
        [Required(ErrorMessage = "Phone number is rquired and must be 11 digit")]
        [StringLength(11, ErrorMessage = "The number must be 11 digit", MinimumLength = 11)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please supply an email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please supply a valid email address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is rquired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "State is rquired")]
        public States State { get; set; }

        [Required(ErrorMessage = "LGA is rquired")]
        public string LGA { get; set; }

        

    }

    public enum States
    {
        select,
        Abia,
        Adamawa,
        Akwa_Ibom,
        Anambra,
        Bauchi,
        Benue,
        Borno,
        Bayelsa,
        Cross_River,
        Delta,
        Ebonyi,
        Edo,
        Ekiti,
        Enugu,
        Federal_Capital_Territory,
        Gombe,
        Imo,
        Jigawa,
        Kebbi,
        Kaduna,
        Kano,
        Kogi,
        Katsina,
        Kwara,
        Lagos,
        Nasarawa,
        Niger,
        Ogun,
        Ondo,
        Osun,
        Oyo,
        Plateau,
        Rivers,
        Sokoto,
        Taraba,
        Yobe,
        Zamfara,
    }
}
