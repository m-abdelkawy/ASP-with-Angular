using Hacienda.Task.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hacienda.Task.MVC.ViewModels
{
    public class PersonFormViewModel
    {
        /*public Person Person { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<City> Cities { get; set; }*/

        #region DataMembers
        public int? Id { get; set; }
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "Country")]
        [ForeignKey("Country")]
        public int? CountryId { get; set; }

        [Required]
        [Display(Name = "City")]
        [ForeignKey("City")]
        public int? CityId { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [ForeignKey("Gender")]
        public int? GenderId { get; set; }

        //[Column(TypeName = "image")]
        public byte[] Image { get; set; }



        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
        #endregion
        public PersonFormViewModel()
        {
            Id = 0;
        }
        public PersonFormViewModel(Person person)
        {
            this.Id = person.Id;
            this.Name = person.Name;
            this.BirthDate = person.BirthDate;
            this.CityId = person.CityId;
            this.GenderId = person.GenderId;
        }
    }
}