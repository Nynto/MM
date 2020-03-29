using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieMakers.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [DisplayName("Phonenumber")]
        public string PhoneNumber { get; set; }
        [DisplayName("Is authorized company")]
        public bool IsAuthorizedCompany { get; set; }
    }
}