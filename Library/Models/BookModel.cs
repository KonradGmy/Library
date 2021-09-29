using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Too long name (max 40 characters)")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Too long author (max 20 characters)")]
        public string Author { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Too long description (max 100 characters)")]
        public string Descritpion { get; set; }

        public BookModel()
        {

        }

        public BookModel(int id, string name, string author, DateTime releaseDate, string descritpion)
        {
            Id = id;
            Name = name;
            Author = author;
            ReleaseDate = releaseDate;
            Descritpion = descritpion;
        }
    }
}
