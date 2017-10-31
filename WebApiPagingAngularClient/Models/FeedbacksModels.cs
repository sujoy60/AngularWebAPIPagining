using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiPagingAngularClient.Models
{
    public class Club
    {

        public int id { get; set; }
        public string user { get; set; }
        public int score { get; set; }
        public string verbatim { get; set; }
    }

    public class ClubPage
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }

    public class ClubQuery
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class ClubQuery1
    {
        public string Name { get; set; }        
    }
}