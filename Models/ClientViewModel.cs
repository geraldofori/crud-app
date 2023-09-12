using System.ComponentModel.DataAnnotations;

namespace CRUD_App.Models
{
    public class ClientViewModel
    {
        [Key]
        public int Id {get; set; }

        public string FirstName {get; set; }

        public string SurName {get; set;}


        public int Age {get; set; }
    }
}