using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetaByte_KellysFeatures2.Models
{
    public class JobAppPostings
    {

        public JobApplicant jobapplicant {get; set;}
        
        public List<JobPosting> jobpostings { get; set; }
    }
}