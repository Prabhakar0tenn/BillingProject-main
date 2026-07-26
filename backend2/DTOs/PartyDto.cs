using backend2.Models;
using System.ComponentModel.DataAnnotations;

namespace backend2.DTOs{
    public class PartyDto{

        [Required(ErrorMessage="Party Name is required")]
        public string partyname{get; set; }=string.Empty;

        [Required(ErrorMessage="Phone Number is required")]
        public string Phone{get; set; }=string.Empty;

        [Required(ErrorMessage="GST Number is required")]
        public string Gstin{get; set; }=string.Empty;

        [Required(ErrorMessage="Address is required")]
        public string address{get; set; }=string.Empty;

    }

                                                                                                                                  
}