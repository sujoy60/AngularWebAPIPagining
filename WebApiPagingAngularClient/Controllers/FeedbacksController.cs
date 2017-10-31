using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiPagingAngularClient.Models;
using WebApiPagingAngularClient.Utility;

namespace WebApiPagingAngularClient.Controllers
{
    [RoutePrefix("api/clubs")]
    public class FeedbacksController : ApiController
    {
        private FeedbackRepository feedbackRepository;

        public FeedbacksController(): this(new FeedbackRepository())
        {

        }

        public FeedbacksController(FeedbackRepository repository)
        {
            this.feedbackRepository = repository;
        }

        // GET: api/Clubs
        [Route("")]
        public IHttpActionResult Get()
        {
            
            var clubs = this.feedbackRepository.Clubs.ToList();

            return Ok(clubs);
        }

        // GET: api/Clubs/5
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var club = this.feedbackRepository.Clubs.FirstOrDefault(c => c.id == id);
            return Ok(club);
        }

        // GET: api/Clubs/clubName
        [Route("{name:alpha}")]
        public IHttpActionResult Get(string user) 
        {
            var club = this.feedbackRepository.Clubs.FirstOrDefault(c => c.user == user);
            return Ok(club);
        }


        // GET: api/Clubs/pageSize/pageNumber/orderBy(optional)
        [Route("{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public IHttpActionResult Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var totalCount = this.feedbackRepository.Clubs.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            var clubQuery = this.feedbackRepository.Clubs;

            if (QueryHelper.PropertyExists<Club>(orderBy))
            {
                var orderByExpression = QueryHelper.GetPropertyExpression<Club>(orderBy);
                clubQuery = clubQuery.OrderBy(orderByExpression);
            } else
            {
                clubQuery = clubQuery.OrderBy(c => c.id);
            }

            var clubs = clubQuery.Skip((pageNumber - 1) * pageSize)                            
                                    .Take(pageSize)                
                                    .ToList();

            var result = new
            {
                TotalCount = totalCount,
                totalPages = totalPages,
                Clubs = clubs
            };

            return Ok(result);
        }
      
        // POST: api/Clubs
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clubs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clubs/5
        public void Delete(int id)
        {
        }
    }
}
