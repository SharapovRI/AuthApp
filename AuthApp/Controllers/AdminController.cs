using AuthApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Controllers
{
    public class AdminController : Controller
    {
        Model1 db;
        public AdminController(Model1 context)
        {
            db = context;

            foreach (var item in db.posts.ToArray())
            {
                item.employee = db.employee.Where(e => e.PostId == item.idPosts).ToList();
            }

            foreach (var item in db.employee.ToArray())
            {
                item.posts = db.posts.FirstOrDefault(e => e.idPosts == item.PostId);
            }
            
            db.flights.Load();
            db.flights_has_stations.Load();
            db.flights_has_traveltime.Load();
            db.flightcrews.Load();
            db.stations.Load();
            db.traveltime.Load();
            db.pilots.Load();
            db.navigators.Load();
            db.radiooperators.Load();
            db.flightattendants.Load();
        }
        public IActionResult Index()
        {
            db.flights.Load();
            List<InfoAboutFlight> info = new List<InfoAboutFlight>();
            var list = db.flights.Local.ToArray();
            for (int i = 0; i < list.Length; i++)
            {
                var a = GetInfoAboutFlight(list[i]);
                if (a != null)
                {
                    info.AddRange(a);
                }
            }
            return View(info);
        }

        public List<InfoAboutFlight> GetInfoAboutFlight(flights flight)
        {
            List<InfoAboutFlight> infoAboutFlights = new List<InfoAboutFlight>();
            List<traveltime> listOfTimes = db.traveltime.Include(c => c.flights_has_traveltime.Where(fl =>fl.Flights_idFlights == flight.idFlights)).ThenInclude(sc => sc.flights).ToList();
            List<stations> listOfStations = db.stations.Include(c => c.flights_has_stations.Where(fl => fl.Flights_idFlights == flight.idFlights)).ThenInclude(sc => sc.flights).ToList();
            foreach (var item in listOfTimes)
            {
                infoAboutFlights.Add(new InfoAboutFlight(flight.idFlights, listOfStations.First().Name, listOfStations.Last().Name, item.ArrivalTime, item.DepartureTime, flight));
            }

            return infoAboutFlights;
        }

        [HttpGet]
        public IActionResult EditFlight(int? flightID) 
        {                   
           if (flightID == null) 
                return RedirectToAction("Index");
            var flight = db.flights.FirstOrDefault(fl => fl.idFlights == flightID);
            //flight.flightcrews = db.flightcrews.FirstOrDefault(e => e.idFlightCrews == flight.FlightCrewId);
            //flight.flightcrews.pilots = db.pilots.FirstOrDefault(e => e.idPilot == flight.flightcrews.PilotId);
            //flight.flightcrews.navigators = db.navigators.FirstOrDefault(e => e.idNavigators == flight.flightcrews.NavigatorId);
            //flight.flightcrews.radiooperators = db.radiooperators.FirstOrDefault(e => e.idRadioOperators == flight.flightcrews.RadioOperatorId);
            //flight.flightcrews.flightattendants = db.flightattendants.FirstOrDefault(e => e.idFlightAttendants == flight.flightcrews.FlightAttendantsId);
            return View(flight); 
        }
    }
}
