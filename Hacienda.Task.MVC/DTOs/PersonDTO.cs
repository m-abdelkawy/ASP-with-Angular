using Hacienda.Task.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hacienda.Task.MVC.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? CityId { get; set; }

        public int? GenderId { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }
        

        public virtual Gender Gender { get; set; }

        public int? CountryId { get; set; }

        public City City { get; set; }
    }
}