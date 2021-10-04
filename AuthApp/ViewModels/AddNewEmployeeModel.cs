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
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Name must have only letters")]
        [MinLength(3 , ErrorMessage = "Name must have more than 2 letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан Surname")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Surname must have only letters")]
        [MinLength(3, ErrorMessage = "Surname must have more than 2 letters")]
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
