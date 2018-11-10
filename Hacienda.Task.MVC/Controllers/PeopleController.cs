using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hacienda.Task.DAL;
using Hacienda.Task.MVC.DTOs;
using Hacienda.Task.MVC.ViewModels;
using Hacienda.Task.Repository.Repositories;

namespace Hacienda.Task.MVC.Controllers
{
    public class PeopleController : Controller
    {
        //private Model1 db = new Model1();
        UnitOfWork uow = new UnitOfWork();

        // GET: People
        public ActionResult Index()
        {
            //var people = db.People.Include(p => p.City).Include(p => p.Gender);
            var people = uow.PersonRepository.GetAll().Include(p => p.City).Include(p => p.Gender);
            return View(people.ToList());
        }

        public ActionResult EditView()
        {
            return View("Edit");
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Person person = db.People.Find(id);
            Person person = uow.PersonRepository.GetById(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult New()
        {
            ViewBag.CountryList = new SelectList(uow.CountryRepository.GetAll(), "Id", "Name");

            //uow.CountryRepository.GetById(ViewBag.CountryList.)

            ViewBag.CityId = new SelectList(uow.CityRepository.GetAll(), "Id", "Name");
            ViewBag.GenderId = new SelectList(uow.GenderRepository.GetAll(), "Id", "Name");

            PersonFormViewModel formVM = new PersonFormViewModel()
            {
                //Countries = uow.CountryRepository.GetAll().ToList(),
                //Cities = uow.CityRepository.GetAll().ToList(),
                //Genders = uow.GenderRepository.GetAll().ToList()
            };
            return View("PersonForm", formVM);
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "Id,Name,BirthDate,CityId,GenderId,Image")] Person person)
        {//Model Binding: Binding PersonViewModel or Person data from request
            if (!ModelState.IsValid)
            {
                PersonFormViewModel formVM = new PersonFormViewModel()
                {
                    Countries = uow.CountryRepository.GetAll().ToList(),
                    Cities = uow.CityRepository.GetAll().ToList(),
                    Genders = uow.GenderRepository.GetAll().ToList()
                };
                return View("PersonForm", formVM);
            }
            if (person.Id == 0)
            {
                uow.PersonRepository.Add(person);
                uow.Commit();
            }
            else
            {
                Person personInDB = uow.PersonRepository.GetById(person.Id);
                uow.PersonRepository.Update(person, uow);
            }

            ViewBag.CityId = new SelectList(uow.CityRepository.GetAll(), "Id", "Name", person.CityId);
            ViewBag.GenderId = new SelectList(uow.GenderRepository.GetAll(), "Id", "Name", person.GenderId);
            return RedirectToAction("Index", "People");
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.CountryList = new SelectList(uow.CountryRepository.GetAll(), "Id", "Name");

            //uow.CountryRepository.GetById(ViewBag.CountryList.)

            ViewBag.CityId = new SelectList(uow.CityRepository.GetAll(), "Id", "Name");
            ViewBag.GenderId = new SelectList(uow.GenderRepository.GetAll(), "Id", "Name");

            Person personInDB = uow.PersonRepository.GetById(id);
            if (personInDB == null)
            {
                return HttpNotFound();
            }

            PersonDTO personDTO = AutoMapper.Mapper.Map<Person, PersonDTO>(personInDB);
            //PersonFormViewModel formVM = new PersonFormViewModel()
            //{
            //    Countries = uow.CountryRepository.GetAll().ToList(),
            //    Cities = uow.CityRepository.GetAll().ToList(),
            //    Genders = uow.GenderRepository.GetAll().ToList()
            //};

            return View("Edit", personDTO);
        }


        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Person person = db.People.Find(id);

            Person person = uow.PersonRepository.GetById(id);

            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult GetCitiesddl(int cid)
        {
            List<City> CitySelectedList = uow.CityRepository.GetAll().Where(c => c.CountryId == cid).ToList();
            ViewBag.CityList = new SelectList(CitySelectedList, "Id", "Name");
            return PartialView("GetCitiesddl");
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Person person = db.People.Find(id);
            //db.People.Remove(person);
            //db.SaveChanges();

            Person person = uow.PersonRepository.GetById(id);
            uow.PersonRepository.Delete(person);
            uow.Commit();

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        uow.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
