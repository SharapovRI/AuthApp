using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.ViewModels
{
    public class AddNewEmployeeModel
    {
        [Required(ErrorMessage = "Не указан Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан Surname")]
        public string Surname { get; set; }

        public Posts Post { get; set; }
    }

    public enum Posts
    {
        [Display(Name = "Pilot")]
        pilots,
        [Display(Name = "Radiooperator")]
        radiooperators,
        [Display(Name = "Navigator")]
        navigators,
        [Display(Name = "Flight Attendant")]
        flightattendants
    }
}
