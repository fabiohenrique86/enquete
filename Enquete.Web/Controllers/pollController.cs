using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Enquete.DTO;
using Enquete.Business.Exceptions;
using Enquete.Business;

namespace Enquete.Web.Controllers
{
    public class PollController : ApiController
    {
        private pollBusiness pollBusiness;

        public PollController()
        {
            pollBusiness = new pollBusiness();
        }
        
        [HttpPost]
        public HttpResponseMessage Poll(poll poll)
        {
            var httpResponseMessage = new HttpResponseMessage();

            try
            {
                var poll_id = pollBusiness.Poll(poll);

                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, new { poll_id = poll_id });
            }
            catch (BusinessException ex)
            {
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
            catch (Exception ex)
            {
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return httpResponseMessage;
        }

        [HttpGet]
        public HttpResponseMessage Get(long id)
        {
            var httpResponseMessage = new HttpResponseMessage();

            try
            {
                var poll = pollBusiness.Get(new poll() { poll_id = id });

                if (poll == null)
                {
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.NotFound, $"Poll {id} not found.");
                }
                else
                {
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        poll_id = poll.poll_id,
                        poll_description = poll.poll_description,
                        options = poll.options.Select(x => new { x.option_id, x.option_description })
                    });
                }
            }
            catch (BusinessException ex)
            {
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
            catch (Exception ex)
            {
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return httpResponseMessage;
        }

        [HttpPost]
        public HttpResponseMessage Vote(long id, [FromBody] optionDTO optionDTO)
        {
            var httpResponseMessage = new HttpResponseMessage();

            try
            {
                var poll = new poll();

                poll.poll_id = id;
                poll.options.Add(optionDTO);

                var polls = pollBusiness.GetAll(poll);

                if (polls == null || polls.Count <= 0)
                {
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    pollBusiness.Vote(poll);

                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, "Vote registered.");
                }
            }
            catch (BusinessException ex)
            {
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
            catch (Exception ex)
            {
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return httpResponseMessage;
        }

        [HttpGet]
        public HttpResponseMessage Stats(long id)
        {
            var httpResponseMessage = new HttpResponseMessage();

            try
            {
                var poll = pollBusiness.Stats(new poll() { poll_id = id });

                if (poll == null)
                {
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.NotFound, $"Poll {id} not found.");
                }
                else
                {
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        views = poll.views,
                        votes = poll.options.Select(x => new { x.option_id, x.qty })
                    });
                }
            }
            catch (BusinessException ex)
            {
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
            catch (Exception ex)
            {
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return httpResponseMessage;
        }
    }
}
