namespace CRUD_App.Models.Domain
{
    public class Member
    {
        public Guid Id {get; set; }
        public string FirstName {get; set; }

        public string MiddleName {get; set;}

        public string SurName {get; set;}

        public int Age {get; set; }


    }
    
}