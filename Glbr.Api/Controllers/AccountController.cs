using Glbr.Api.Models.Account;
using Glbr.Common.Resources;
using Glbr.Domain.Contracts.Services;
using Glbr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Glbr.Api.Controllers
{
    [RoutePrefix("api/users")]
    public class AccountController : ApiController
    {
        private IUserService _service;

        public AccountController(IUserService service)
        {
            this._service = service;
        }

        [HttpPost]
        [Route("")]
        public Task<HttpResponseMessage> Post(RegisterUserModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.Register(model.Name, model.Email, model.Password, model.ConfirmPassword);
                response = Request.CreateResponse(HttpStatusCode.OK, new { name = model.Name, email = model.Email });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Authorize]
        [HttpPut]
        [Route("")]
        public Task<HttpResponseMessage> Put(ChangeInformationModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.ChangeInformation(User.Identity.Name, model.Name);
                response = Request.CreateResponse(HttpStatusCode.OK, new { name = model.Name });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /*//[Authorize]
        [HttpPut]
        [Route("disable")]
        public Task<HttpResponseMessage> Put(string thisUserEmail)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.Disable(thisUserEmail);
                response = Request.CreateResponse(HttpStatusCode.OK, "Usuário bloqueado.");
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }*/

        [HttpPut]
        [Route("setUser")]
        public Task<HttpResponseMessage> PutSetUser(string thisUserEmail)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.SetAsUser(thisUserEmail);
                response = Request.CreateResponse(HttpStatusCode.OK, "Usuário se tornou solicitante.");
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPut]
        [Route("setCustomerData")]
        public Task<HttpResponseMessage> PutCustomerData(string thisUserEmail, Customer customer)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.RegisterCustomerData(thisUserEmail, customer);
                response = Request.CreateResponse(HttpStatusCode.OK, "Cadastro de cliente atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPut]
        [Route("setAdm")]
        public Task<HttpResponseMessage> PutSetAdm(string thisUserEmail)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.SetAsAdmin(thisUserEmail);
                response = Request.CreateResponse(HttpStatusCode.OK, "Usuário se tornou administrador.");
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Authorize]
        [HttpPost]
        [Route("changepassword")]
        public Task<HttpResponseMessage> ChangePassword(ChangePasswordModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.ChangePassword(User.Identity.Name, model.Password, model.NewPassword, model.ConfirmNewPassword);
                response = Request.CreateResponse(HttpStatusCode.OK, Messages.PasswordSuccessfulyChanges);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        [Route("resetpassword")]
        public Task<HttpResponseMessage> ResetPassword(ResetPasswordModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var password = _service.ResetPassword(model.Email);
                response = Request.CreateResponse(HttpStatusCode.OK, String.Format(Messages.ResetPasswordEmailBody, password));
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
