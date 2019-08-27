using System.ComponentModel.DataAnnotations;

namespace DiamondFitnessGym.Controllers
{
  
        public class MailModel
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string secondName { get; set; }
            [Required]
            public string Phone { get; set; }
            public string NationalId { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            [Required]
            public string Period { get; set; }
            public string ExtraServices { get; set; }
            [Required]
            public string Age { get; set; }
            public string Health { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
       
    }
}