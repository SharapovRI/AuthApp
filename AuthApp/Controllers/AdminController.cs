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
            List<traveltime> listOfTimes = db.flights_has_traveltime.Where(e => e.Flights_idFlights == flight.idFlights).Select(e => e.traveltime).ToList();
            List<stations> listOfStations = db.flights_has_stations.Where(e => e.Flights_idFlights == flight.idFlights).Select(e => e.stations).ToList();
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

        public IActionResult DetailsStations(int id)
        {
            flights flight = db.flights.FirstOrDefault(e => e.idFlights == id);
            if (flight != null)
                return PartialView(flight);
            return Accepted();
        }

        public IActionResult CrewChanging(int flightID)
        {
            flights flight = db.flights.FirstOrDefault(e => e.idFlights == flightID);
            List<flightcrews> freeCrews = db.flightcrews.Where(e => e.flights == null).ToList();
            if (flight.flightcrews != null)
            {
                freeCrews.Add(flight.flightcrews);
            }
            ViewBag.ThisFlight = flight;
            return View(freeCrews);
        }

        
        public async Task<IActionResult> SaveChangedCrew(int crewID, int flightID)
        {
            flights flight = db.flights.FirstOrDefault(e => e.idFlights == flightID);
            flightcrews flightcrew = db.flightcrews.FirstOrDefault(e => e.idFlightCrews == crewID);
            flight.flightcrews = flightcrew;
            db.flights.Update(flight);
            await db.SaveChangesAsync();
            return RedirectToAction("EditFlight", new { flightID = flightID });
        }

        public IActionResult CrewDeleting(int flightID)
        {
            flights flight = db.flights.FirstOrDefault(e => e.idFlights == flightID);
            flight.flightcrews = null;
            db.flights.Update(flight);
            db.SaveChanges();
            return RedirectToAction("EditFlight", new { flightID = flightID });
        }
    }
}
