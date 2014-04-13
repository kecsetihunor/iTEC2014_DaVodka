using iTEC.Services.AccessLayer;
using iTEC.Services.AccessLayer.DelightService;
using iTEC.WebApplication.Models;
using iTEC.WebApplication.Models.Controls.InfiniteScroller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iTEC.WebApplication.Controllers
{
    [Authorize]
    public class VoteController : SafeApiController
    {
        [HttpGet]
        public HttpResponseMessage GetVotes()
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var votes = service.GetVotes();
                if (votes == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<IEnumerable<VotedProductDTO>>(HttpStatusCode.OK, votes);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage GetVote([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var vote = service.GetVote(model.Id);
                if (vote == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<VoteDTO>(HttpStatusCode.OK, vote);
                return response;
            }, model);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteVote([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                service.DeleteVote(model.Id);
                var response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }, model);
        }

        [HttpPost]
        public HttpResponseMessage SaveVote(VoteDTO vote)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                int id = service.SaveVote(vote);
                if (id <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<int>(HttpStatusCode.OK, id);
                return response;
            }, vote);
        }

        [HttpPost]
        public HttpResponseMessage Vote([FromBody] VoteModel model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                int remainingPoints = service.Vote(User.Identity.Name, model.ProductId, model.Points);
                var response = Request.CreateResponse<int>(HttpStatusCode.OK, remainingPoints);
                return response;
            }, model);
        }

        [HttpPost]
        public HttpResponseMessage GetAllVotedPoints()
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                int allPoints = service.GetAllVotedPoints();
                var response = Request.CreateResponse<int>(HttpStatusCode.OK, allPoints);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage GetAvailablePoints()
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                int availablePoints = service.GetAvailablePoints(User.Identity.Name);
                var response = Request.CreateResponse<int>(HttpStatusCode.OK, availablePoints);
                return response;
            });
        }
    }
}
