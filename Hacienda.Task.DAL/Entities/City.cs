using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hacienda.Task.DAL.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [ForeignKey("Country")]
        public int? CountryId { get; set; }

        public Country Country { get; set; }
    }
}
