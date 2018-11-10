using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Hacienda.Task.DAL;
using Hacienda.Task.Repository.Repositories;

namespace Hacienda.Task.API.Controllers
{
    public class PeopleController : ApiController
    {
        private UnitOfWork uow = new UnitOfWork();

        // GET: api/People
        public IQueryable<Person> GetPersons()
        {
            return uow.PersonRepository.GetAll();
        }

        // GET: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(int id)
        {
            Person person = uow.PersonRepository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerson(int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            //db.Entry(person).State = EntityState.Modified;
            uow.PersonRepository.Update(person, uow);

            try
            {
                //db.SaveChanges();
                uow.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/People
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Persons.Add(person);
            //db.SaveChanges();

            uow.PersonRepository.Add(person);
            uow.Commit();

            return CreatedAtRoute("DefaultApi", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(int id)
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
    }
}