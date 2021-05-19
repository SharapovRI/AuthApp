using AuthApp.Models;
using AuthApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Controllers
{
    public class ManagerController : Controller
    {
        Model1 db;
        public ManagerController(Model1 context)
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

            db.pilots.Load();
            db.navigators.Load();
            db.radiooperators.Load();
            db.flightattendants.Load();
            db.flightcrews.Load();
            db.flights.Load();
        }
        public IActionResult Index()
        {
            var list = db.flightcrews.ToList();
            return View(list);
        }

        public IActionResult AddNewCrew()
        {
            IEnumerable<SelectListItem> freePilots = from pil in db.pilots
                                                     where pil.flightcrew == null
                                                     select new SelectListItem { Text = pil.Name + " " + pil.Surname, Value = pil.idPilot.ToString() };
            IEnumerable<SelectListItem> freeNavigators = from nav in db.navigators
                                                         where nav.flightcrew == null
                                                         select new SelectListItem { Text = nav.Name + " " + nav.Surname, Value = nav.idNavigators.ToString() };
            IEnumerable<SelectListItem> freeRadiooperators = from rad in db.radiooperators
                                                             where rad.flightcrew == null
                                                             select new SelectListItem { Text = rad.Name + " " + rad.Surname, Value = rad.idRadioOperators.ToString() };
            IEnumerable<SelectListItem> freeFlightattendants = from att in db.flightattendants
                                                               where att.flightcrew == null
                                                               select new SelectListItem { Text = att.Name + " " + att.Surname, Value = att.idFlightAttendants.ToString() };

            ViewBag.FreePilots = freePilots.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" });

            ViewBag.FreeNavigators = freeNavigators.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" });

            ViewBag.FreeRadiooperators = freeRadiooperators.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" });

            ViewBag.FreeFlightattendants = freeFlightattendants.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewCrewSave(EditCrewModel model)
        {
            flightcrews flightcrew = new flightcrews();
            if (model.PilotId != -1)
            {
                flightcrew.PilotId = model.PilotId;
            }
            else flightcrew.PilotId = null;

            if (model.NavigatorId != -1)
            {
                flightcrew.NavigatorId = model.NavigatorId;
            }
            else flightcrew.NavigatorId = null;

            if (model.RadiooperatorId != -1)
            {
                flightcrew.RadioOperatorId = model.RadiooperatorId;
            }
            else flightcrew.RadioOperatorId = null;

            if (model.FlightattendantId != -1)
            {
                flightcrew.FlightAttendantsId = model.FlightattendantId;
            }
            else flightcrew.FlightAttendantsId = null;

            db.flightcrews.Add(flightcrew);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Manager");
        }

        [HttpGet]
        public IActionResult EditCrew(int flightID)
        {
            flightcrews flightcrew = db.flightcrews.FirstOrDefault(e => e.idFlightCrews == flightID);
            IEnumerable<SelectListItem> freePilots = from pil in db.pilots
                                                     where pil.flightcrew == null
                                                     select new SelectListItem { Text = pil.Name + " " + pil.Surname, Value = pil.idPilot.ToString() };
            IEnumerable<SelectListItem> freeNavigators = from nav in db.navigators
                                                     where nav.flightcrew == null
                                                     select new SelectListItem { Text = nav.Name + " " + nav.Surname, Value = nav.idNavigators.ToString() };
            IEnumerable<SelectListItem> freeRadiooperators = from rad in db.radiooperators
                                                         where rad.flightcrew == null
                                                         select new SelectListItem { Text = rad.Name + " " + rad.Surname, Value = rad.idRadioOperators.ToString() };
            IEnumerable<SelectListItem> freeFlightattendants = from att in db.flightattendants
                                                             where att.flightcrew == null
                                                             select new SelectListItem { Text = att.Name + " " + att.Surname, Value = att.idFlightAttendants.ToString() };
            ViewBag.Flightcrew = flightcrew;
            if (flightcrew.pilots != null)
            {
                ViewBag.FreePilots = freePilots.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" })
                    .Prepend(new SelectListItem { Text = flightcrew.pilots.Name + " " + flightcrew.pilots.Surname, Value = flightcrew.pilots.idPilot.ToString() });
            }
            else ViewBag.FreePilots = freePilots.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" });

            if (flightcrew.navigators != null)
            {
                ViewBag.FreeNavigators = freeNavigators.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" })
                .Prepend(new SelectListItem { Text = flightcrew.navigators.Name + " " + flightcrew.navigators.Surname, Value = flightcrew.navigators.idNavigators.ToString() });
            }
            else ViewBag.FreeNavigators = freeNavigators.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" });

            if (flightcrew.radiooperators != null)
            {
                ViewBag.FreeRadiooperators = freeRadiooperators.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" })
                    .Prepend(new SelectListItem { Text = flightcrew.radiooperators.Name + " " + flightcrew.radiooperators.Surname, Value = flightcrew.radiooperators.idRadioOperators.ToString() });
            }
            else ViewBag.FreeRadiooperators = freeRadiooperators.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" });

            if (flightcrew.flightattendants != null)
            {
                ViewBag.FreeFlightattendants = freeFlightattendants.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" })
                .Prepend(new SelectListItem { Text = flightcrew.flightattendants.Name + " " + flightcrew.flightattendants.Surname, Value = flightcrew.flightattendants.idFlightAttendants.ToString() });
            }
            else ViewBag.FreeFlightattendants = freeFlightattendants.Prepend(new SelectListItem { Text = "      -      ", Value = "-1" });
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCrewSave(EditCrewModel model)
        {
            if (ModelState.IsValid)
            {
                flightcrews flightcrew = db.flightcrews.FirstOrDefault(e => e.idFlightCrews == model.Id);
                flightcrew.pilots = await db.pilots.FirstOrDefaultAsync(e => e.idPilot == model.PilotId);
                flightcrew.navigators = await db.navigators.FirstOrDefaultAsync(e => e.idNavigators == model.NavigatorId);
                flightcrew.radiooperators = await db.radiooperators.FirstOrDefaultAsync(e => e.idRadioOperators == model.RadiooperatorId);
                flightcrew.flightattendants = await db.flightattendants.FirstOrDefaultAsync(e => e.idFlightAttendants == model.FlightattendantId);
                db.flightcrews.Update(flightcrew);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Manager");
        }

        
        public async Task<IActionResult> DeleteCrew(int flightCrewId)
        {
            flightcrews flightcrew = await db.flightcrews.FirstOrDefaultAsync(e => e.idFlightCrews == flightCrewId);
            if (flightcrew != null)
            {
                db.flightcrews.Remove(flightcrew);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Manager");
        }

        public IActionResult AddNewEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adding(AddNewEmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Post.ToString())
                {
                    case "pilots":
                        {
                            await db.pilots.AddAsync(new pilots() { Name = model.Name, Surname = model.Surname });
                            db.SaveChanges();
                            break;
                        }
                    case "radiooperators":
                        {
                            await db.radiooperators.AddAsync(new radiooperators() { Name = model.Name, Surname = model.Surname });
                            db.SaveChanges();
                            break;
                        }
                    case "navigators":
                        {
                            await db.navigators.AddAsync(new navigators() { Name = model.Name, Surname = model.Surname });
                            db.SaveChanges();
                            break;
                        }
                    case "flightattendants":
                        {
                            await db.flightattendants.AddAsync(new flightattendants() { Name = model.Name, Surname = model.Surname });
                            db.SaveChanges();
                            break;
                        }
                    default:
                        break;
                }
            }
            return RedirectToAction("ListOfEmployeers", "Manager");
        }

        public IActionResult ListOfEmployeers()
        {
            List<IEmployee> list = new List<IEmployee>();
            list.AddRange(db.pilots.ToList());
            list.AddRange(db.navigators.ToList());
            list.AddRange(db.radiooperators.ToList());
            list.AddRange(db.flightattendants.ToList());
            return View(list);
        }

        public IActionResult EditEmployee(int iemployee, string type)
        {
            switch (type)
            {
                case "pilots":
                    {
                        ViewBag.EditingEmployee = db.pilots.FirstOrDefault(e => e.idPilot == iemployee);
                        break;
                    }
                case "radiooperators":
                    {
                        ViewBag.EditingEmployee = db.radiooperators.FirstOrDefault(e => e.idRadioOperators == iemployee);
                        break;
                    }
                case "navigators":
                    {
                        ViewBag.EditingEmployee = db.navigators.FirstOrDefault(e => e.idNavigators == iemployee);
                        break;
                    }
                case "flightattendants":
                    {
                        ViewBag.EditingEmployee = db.flightattendants.FirstOrDefault(e => e.idFlightAttendants == iemployee);
                        break;
                    }
                default:
                    break;
            }
            //ViewBag.EditingEmployee = iemployee;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editing(EditEmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Post.ToString())
                {
                    case "pilots":
                        {
                            pilots pilot = db.pilots.FirstOrDefault(e => e.idPilot == model.Ident);
                            pilot.Name = model.Name;
                            pilot.Surname = model.Surname;
                            db.pilots.Update(pilot);
                            await db.SaveChangesAsync();
                            break;
                        }
                    case "radiooperators":
                        {
                            radiooperators radiooperator = db.radiooperators.FirstOrDefault(e => e.idRadioOperators == model.Ident);
                            radiooperator.Name = model.Name;
                            radiooperator.Surname = model.Surname;
                            db.radiooperators.Update(radiooperator);
                            await db.SaveChangesAsync();
                            break;
                        }
                    case "navigators":
                        {
                            navigators navigator = db.navigators.FirstOrDefault(e => e.idNavigators == model.Ident);
                            navigator.Name = model.Name;
                            navigator.Surname = model.Surname;
                            db.navigators.Update(navigator);
                            await db.SaveChangesAsync();
                            break;
                        }
                    case "flightattendants":
                        {
                            flightattendants flightattendant = db.flightattendants.FirstOrDefault(e => e.idFlightAttendants == model.Ident);
                            flightattendant.Name = model.Name;
                            flightattendant.Surname = model.Surname;
                            db.flightattendants.Update(flightattendant);
                            await db.SaveChangesAsync();
                            break;
                        }
                    default:
                        break;
                }
            }
            return RedirectToAction("ListOfEmployeers", "Manager");
        }

        
        public async Task<IActionResult> Deleting(int iemployee, string type)
        {
            switch (type)
            {
                case "pilots":
                    {
                        pilots pilot = db.pilots.FirstOrDefault(e => e.idPilot == iemployee);
                        //pilot.flightcrew.PilotId = null;
                        db.pilots.Remove(pilot);
                        await db.SaveChangesAsync();
                        break;
                    }
                case "radiooperators":
                    {
                        radiooperators radiooperator = db.radiooperators.FirstOrDefault(e => e.idRadioOperators == iemployee);
                        //radiooperator.flightcrew.RadioOperatorId = null;
                        db.radiooperators.Remove(radiooperator);
                        await db.SaveChangesAsync();
                        break;
                    }
                case "navigators":
                    {
                        navigators navigator = db.navigators.FirstOrDefault(e => e.idNavigators == iemployee);
                        //navigator.flightcrew.NavigatorId = null;
                        db.navigators.Remove(navigator);
                        await db.SaveChangesAsync();
                        break;
                    }
                case "flightattendants":
                    {
                        flightattendants flightattendant = db.flightattendants.FirstOrDefault(e => e.idFlightAttendants == iemployee);
                        //flightattendant.flightcrew.FlightAttendantsId = null;
                        db.flightattendants.Remove(flightattendant);
                        await db.SaveChangesAsync();
                        break;
                    }
                default:
                    break;
            }
            return RedirectToAction("ListOfEmployeers", "Manager");
        }
    }
}
