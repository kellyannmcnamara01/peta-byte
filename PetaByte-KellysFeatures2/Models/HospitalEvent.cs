//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetaByte_KellysFeatures2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public partial class HospitalEvent
    {
        public int eventsId { get; set; }

        [Required(ErrorMessage = "Event Name is Required")]
        [DataType(DataType.Text, ErrorMessage = "No numbers are allowed. Please enter text.")]
        [Display(Name = "Name")]
        public string evntName { get; set; }

        [Required(ErrorMessage = "Event Description is Required")]
        [DataType(DataType.Text, ErrorMessage = "No numbers are allowed. Please enter text.")]
        [Display(Name = "Description")]
        public string evntDesc { get; set; }

        [Required(ErrorMessage = "Event Location is Required")]
        [DataType(DataType.Text, ErrorMessage = "No numbers are allowed. Please enter text.")]
        [Display(Name = "Location")]
        public string evntLoc { get; set; }

        [Required(ErrorMessage = "Event Date is Required")]
        [DataType(DataType.Date, ErrorMessage = "No numbers are allowed. Please enter text.")]
        [Display(Name = "Date")]
        public System.DateTime evntDate { get; set; }

        [Required(ErrorMessage = "Event Start Time is Required")]
        [DataType(DataType.Time, ErrorMessage = "No numbers are allowed. Please enter text.")]
        [Display(Name = "Starts")]
        public System.TimeSpan evntTimebg { get; set; }

        [Required(ErrorMessage = "Event End Time is Required")]
        [DataType(DataType.Time, ErrorMessage = "No numbers are allowed. Please enter text.")]
        [Display(Name = "Ends")]
        public System.TimeSpan evntTimefn { get; set; }
    }
}
