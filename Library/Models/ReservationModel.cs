using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class ReservationModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public virtual IdentityUser User { get; set; }
        [Required]
        public virtual BookModel Book { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public ReservationModel()
        {

        }

        public ReservationModel(int id, IdentityUser user, BookModel book, DateTime date)
        {
            Id = id;
            User = user;
            Book = book;
            Date = date;
        }
    }
}
