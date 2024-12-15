using System.ComponentModel.DataAnnotations;

namespace Balc_Bogdan_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Author")]
        public string AuthorFullName => $"{FirstName} {LastName}";
        public ICollection<Book>? Books { get; set; }

    }
}
