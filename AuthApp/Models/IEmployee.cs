using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Models
{
    public interface IEmployee
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
