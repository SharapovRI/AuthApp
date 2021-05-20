using AuthApp.Models;
using AuthApp.ViewModels;
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
            List<stations> listOfStations = db.flights_has_stations.OrderBy(e => e.NumberofStation).Where(e => e.Flights_idFlights == flight.idFlights).Select(e => e.stations).ToList();
            infoAboutFlights.Add(new InfoAboutFlight(flight.idFlights, listOfStations.First().Name, listOfStations.Last().Name, flight, flight.RouteId));

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


        public async Task<IActionResult> DeleteFlight(int flightID)
        {
            flights flight = db.flights.FirstOrDefault(e => e.idFlights == flightID);
            db.flights.Remove(flight);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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

        public IActionResult EditRoute(int flightID)
        {
            flights flight = db.flights.FirstOrDefault(e => e.idFlights == flightID);
            List<stations> a = db.stations.ToList();
            foreach (var item in flight.flights_has_stations.Select(e => e.stations).ToList())
            {
                a.Remove(item);
            }
            ViewBag.FreeStations = a;
            return View(flight);
        }

        public IActionResult DeleteStation(int stationID, int flightID)
        {
            flights flight = db.flights.FirstOrDefault(e => e.idFlights == flightID);
            flights_has_stations flights_Has_Station = flight.flights_has_stations.FirstOrDefault(e => e.Stations_IdStations == stationID);
            db.flights_has_stations.Remove(flights_Has_Station);
            db.SaveChanges();
            return RedirectToAction("EditRoute", new { flightID = flightID });
        }

        public IActionResult AddStation(int stationID, int flightID)
        {
            flights flight = db.flights.FirstOrDefault(e => e.idFlights == flightID);
            int lastStation = flight.flights_has_stations.Max(e => e.NumberofStation);
            db.flights_has_stations.Add(new flights_has_stations { Flights_idFlights = flightID, Stations_IdStations = stationID, NumberofStation = lastStation + 1 });
            db.SaveChanges();
            return RedirectToAction("EditRoute", new { flightID = flightID });
        }

        ////////////////////Time
        ///
        public IActionResult EditTravelTime(int timeID, int flightID)
        {
            traveltime traveltime = db.traveltime.FirstOrDefault(e => e.idTravelTime == timeID);
            ViewBag.TravelTime = traveltime;
            ViewBag.FlightID = flightID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTime(EditTravelTimeModel model, int flightID)
        {
            if (ModelState.IsValid)
            {
                if (model.ID != 0)
                {
                    traveltime traveltime = db.traveltime.FirstOrDefault(e => e.idTravelTime == model.ID);
                    traveltime.ArrivalTime = model.ArrTime;
                    traveltime.DepartureTime = model.DepTime;
                    db.traveltime.Update(traveltime);
                    await db.SaveChangesAsync();
                }
                else
                {
                    traveltime traveltime = new traveltime() { ArrivalTime = model.ArrTime, DepartureTime = model.DepTime, Flights_idFlights = flightID };
                    db.traveltime.Add(traveltime);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("EditFlight", new { flightID = flightID });
        }

        public IActionResult AddTravelTime(int flightID)
        {
            ViewBag.FlightID = flightID;
            return View();
        }
        public IActionResult DeleteTravelTime(int timeID, int flightID)
        {
            traveltime traveltime = db.traveltime.FirstOrDefault(e => e.idTravelTime == timeID);
            db.traveltime.Remove(traveltime);
            db.SaveChanges();
            return RedirectToAction("EditFlight", new { flightID = flightID });
        }
    }
}
