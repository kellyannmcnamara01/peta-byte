//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace PetaByte_KellysFeatures2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Donation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donation()
        {
            this.DonationHonours = new HashSet<DonationHonour>();
        }
    
        public int donationsId { get; set; }

        [Display(Name = "Donation Amount")]
        [Required(ErrorMessage = "Donation Amount is required")]
        public decimal donationAmount { get; set; }

        public Nullable<int> occurenceId { get; set; }

        [Display(Name = "Donation Date")]
        [Required(ErrorMessage = "Donation Date is required")]
        public System.DateTime donationDate { get; set; }

        public Nullable<int> donorsId { get; set; }

        [Display(Name = "Donor First Name Amount")]
        [Required(ErrorMessage = "Donor First Name is required")]
        public string donorFN { get; set; }

        [Display(Name = "Donor Last Name")]
        [Required(ErrorMessage = "Donation Last Name is required")]
        public string donorLN { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Home Phone")]
        public string homeNum { get; set; }

        [Display(Name = "Work Phone")]
        public string workNum { get; set; }

        [Display(Name = "Mobile Phone")]
        public string mobileNum { get; set; }

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Street Address is required")]
        public string addressStreet { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Province is required")]
        public string addressProv { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        public string addressCountry { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal Code is required")]
        public string postal { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        public string addressCity { get; set; }

        [Display(Name = "Honouree First Name")]
        public string honorFN { get; set; }

        [Display(Name = "Honouree Last Name")]
        public string honorLN { get; set; }

        [Display(Name = "Company")]
        public string company { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonationHonour> DonationHonours { get; set; }
        public virtual DonationOccurence DonationOccurence { get; set; }
        public virtual Donor Donor { get; set; }
    }
}
