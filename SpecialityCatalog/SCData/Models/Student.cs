using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCData.Models
{
    public class Student : IItem
    {
        [Key]
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [ForeignKey(nameof(Group))]
        public int GroupId { get; set; }

        public Group? Group { get; set; }


        [ForeignKey(nameof(Direction))]
        public int DirectionId { get; set; }

        public Direction? Direction { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public Country? Country { get; set; }

    }
}
