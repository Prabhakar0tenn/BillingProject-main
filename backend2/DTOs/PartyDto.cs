using backend2.Models;
using System.ComponentModel.DataAnnotations;

namespace backend2.DTOs{
    public class PartyDto{

        [Required(ErrorMessage="Party Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Party Name must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s.\-]+$", ErrorMessage = "Party Name must not contain special characters")]
        public string partyname{get; set; }=string.Empty;

        [Required(ErrorMessage="Phone Number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Number must be exactly 10 digits")]
        public string Phone{get; set; }=string.Empty;

        [Required(ErrorMessage="GST Number is required")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "GST Number must be exactly 15 characters")]
        [RegularExpression(@"^[0-9]{2}[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}[1-9A-Za-z]{1}[zZ][0-9A-Za-z]{1}$", ErrorMessage = "GST Number must be a valid 15-character GSTIN (e.g. 22AAAAA0000A1Z5)")]
        public string Gstin{get; set; }=string.Empty;

        [Required(ErrorMessage="Address is required")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 250 characters")]
        public string address{get; set; }=string.Empty;

    }
}