using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.ViewModels
{
    public class EditEmployeeModel
    {

        [Required(ErrorMessage = "Не указан Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан Surname")]
        public string Surname { get; set; }

        public Posts Post { get; set; }

        [Required(ErrorMessage = "Error")]
        public int Ident { get; set; }
    }
}
