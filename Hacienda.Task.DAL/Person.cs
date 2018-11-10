namespace Hacienda.Task.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person")]
    public partial class Person
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name ="Date of Birth")]
        public DateTime? BirthDate { get; set; }

        [Display(Name ="City")][ForeignKey("City")]
        public int? CityId { get; set; }

        [Display(Name ="Gender")]
        public int? GenderId { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public virtual City City { get; set; }

        public virtual Gender Gender { get; set; }
    }
}
