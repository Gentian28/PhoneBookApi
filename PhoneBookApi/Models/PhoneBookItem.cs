using System.ComponentModel.DataAnnotations;
using static PhoneBookApi.PhoneTypeEnumeration;

namespace PhoneBookApi.Models
{
    public class PhoneBookItem
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public PhoneType Type { get; set; }

        [Phone]
        [Required]
        public string Number { get; set; }
    }
}