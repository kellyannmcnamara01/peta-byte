using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//tutorial help credit: code.tutsplus.com/courses/building-a-cms-with-aspnet-mvc5 (kinda helped)
//create a generic post for the user to use
namespace PetaByte_KellysFeatures2.Models
{
    public class Post
    {
        //use the url as a unique ID
         public string pageId { get; set; }

        //the title of the page
         public string pageTitle { get; set; }

        //the content of the page
        public string pageContent { get; set; }

        //the data and time of when the page was created
        public DateTime timestamp { get; set; }

        //status for published or not
        public string status { get; set; }

        //employeeId
        public int employeeId { get; set; }

         
    }
}