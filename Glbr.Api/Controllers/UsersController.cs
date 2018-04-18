using Glbr.Api.Attributes;
using Glbr.Domain.Contracts.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Glbr.Api.Controllers
{
    [RoutePrefix("api/customers")]
    public class UserController : ApiController
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("users")]
        [DeflateCompression]
        public Task<HttpResponseMessage> GetUsers()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var result = _service.GetByRange(0, 25);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        [Route("customerData")]
        [DeflateCompression]
        public Task<HttpResponseMessage> GetUserCustomerData()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var thisUser = _service.GetByEmail(User.Identity.Name);
                var result = thisUser.Customer;
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }

            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        [Route("role")]
        [DeflateCompression]
        public Task<HttpResponseMessage> GetUserRole(string thisUserEmail)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var thisUser = _service.GetByEmail(thisUserEmail);
                var result = thisUser.Role.Id;
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
    }
}