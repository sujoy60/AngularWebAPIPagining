using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.string;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WebApiPagingAngularClient.Models
{
    public class ClubRepository
    {
        private List<Club> clubs = new List<Club>();
        
        public List<Club> getData()//dynamic fetchData()//: this(new ClubRepository())
        {
            var webClient = new WebClient();
            var json = webClient.DownloadString(@"https://resonatetest.azurewebsites.net/data");
            var feedbackCollected = JsonConvert.DeserializeObject<List<Club>>(json);

            feedbackCollected.ForEach(i => clubs.Add(new Club { id = i.id, user = i.user, score = i.score, verbatim = i.verbatim }));
            return clubs;
        }


        public IQueryable<Club> Clubs 
        {
          
           get { return this.getData().AsQueryable(); }
           
        }
    }
}