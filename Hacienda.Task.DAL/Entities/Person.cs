using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hacienda.Task.DAL.Entities
{
    public class Person
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public byte?[] Image { get; set; }

        public DateTime? BirthDate { get; set; }


        [Display(Name = "City")]
        [ForeignKey("City")]
        public int? CityId { get; set; }

        public City City { get; set; }

        [Display(Name = "Gender")]
        [ForeignKey("Gender")]
        public int? GenderId { get; set; }

        public Gender Gender { get; set; }
    }
}
