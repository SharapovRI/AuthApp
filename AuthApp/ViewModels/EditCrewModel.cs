using AuthApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.ViewModels
{
    public class EditCrewModel
    {
        public int Id { get; set; }


        public string Pilot { get; set; }

        public string Navigator { get; set; }

        public string Radiooperator { get; set; }

        public string Flightattendant { get; set; }


        public int PilotId
        {
            get
            {
                return int.Parse(Pilot);
            }
        }

        public int NavigatorId
        {
            get
            {
                return int.Parse(Navigator);
            }
        }

        public int RadiooperatorId
        {
            get
            {
                return int.Parse(Radiooperator);
            }
        }

        public int FlightattendantId
        {
            get
            {
                return int.Parse(Flightattendant);
            }
        }
    }
}
