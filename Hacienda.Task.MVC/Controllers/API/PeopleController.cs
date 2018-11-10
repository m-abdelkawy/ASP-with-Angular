using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using AutoMapper;
using Hacienda.Task.DAL;
using Hacienda.Task.MVC.DTOs;
using Hacienda.Task.Repository.Repositories;

namespace Hacienda.Task.MVC.Controllers.API
{
    public class PeopleController : ApiController
    {
        #region CRUD Operations

        private UnitOfWork uow = new UnitOfWork();


        // GET: api/People
        public IQueryable<PersonDTO> GetPersons()
        {
            return uow.PersonRepository.GetAll().Select(Mapper.Map<Person, PersonDTO>).AsQueryable();
        }

        // GET: api/People/5
        //[ResponseType(typeof(Person))]
        public PersonDTO GetPerson(int id)
        {
            Person person = uow.PersonRepository.GetById(id);
            if (person == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Person, PersonDTO>(person);
        }

        // PUT: api/People/5
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdatePerson(int id, PersonDTO personDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Person personInDB = uow.PersonRepository.GetById(id);

            if (personInDB == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<PersonDTO, Person>(personDTO, personInDB);
            personInDB.Id = id;
            //db.Entry(person).State = EntityState.Modified;
            //uow.PersonRepository.Update(person, uow);

            uow.Commit();

            return CreatedAtRoute("DefaultApi", new { id = personInDB.Id }, personInDB);

        }

        // POST: api/People
        [System.Web.Http.HttpPost]
        public PersonDTO AddPerson(PersonDTO personDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            //db.Persons.Add(person);
            //db.SaveChanges();
            Person person = Mapper.Map<PersonDTO, Person>(personDTO);
            uow.PersonRepository.Add(person);
            uow.Commit();

            personDTO.Id = person.Id;

            return personDTO;
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            //Person person = db.Persons.Find(id);
            Person person = uow.PersonRepository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            //db.Persons.Remove(person);
            //db.SaveChanges();

            uow.PersonRepository.Delete(id);
            uow.Commit();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            //return db.Persons.Count(e => e.Id == id) > 0;
            return uow.PersonRepository.GetAll().Count(p => p.Id == id) > 0;
        }

        #endregion

        #region Views
        //public ActionResult New()
        //{
        //    return View()
        //}

        #endregion
    }
}