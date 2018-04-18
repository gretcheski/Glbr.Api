using Glbr.Domain.Entities;
using Glbr.Infra.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Glbr.Api.Controllers
{
    [RoutePrefix("api/sales")]
    public class SalesController : ApiController
    {
        private GlbrDataContext db = new GlbrDataContext();


        [Route("")]
        public HttpResponseMessage GetSales()
        {
            var result = db.Sales.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // vendas acima de valor x
        [Route("totalValueBigger")]
        public HttpResponseMessage GetSalesBiggerThanTotalValue(int totalValue)
        {
            var result = db.Sales.Where(x => x.TotalValue >= totalValue).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // vendas abaixo de valor x
        [Route("totalValueSmaller")]
        public HttpResponseMessage GetSalesSmallerThanTotalValue(int totalValue)
        {
            var result = db.Sales.Where(x => x.TotalValue <= totalValue).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // histórico do cliente
        [Route("customerhistory")]
        public HttpResponseMessage GetCustomerHistory()
        {
            var user = db.Users.First(x => x.Email == User.Identity.Name);
            var result = db.Sales.Where(x => x.Customer == user.Customer).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("{saleId}")]
        [ResponseType(typeof(Sale))]
        public IHttpActionResult GetSale(int saleId)
        {
            Sale sale = db.Sales.Find(saleId);
            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        // delivery postada pelo usuário
        [HttpPost]
        [Route("postbyuser")]
        public HttpResponseMessage PostSales(Sale sale)
        {
            if (sale == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var user = db.Users.First(x => x.Email == User.Identity.Name);
                sale.Customer = user.Customer;
                sale.SaleDate = DateTime.Now;
                db.Sales.Add(sale);
                db.SaveChanges();

                var result = sale;
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao cadastrar compra.");
            }

        }

        [HttpPut]
        [Route("sales")]
        public HttpResponseMessage PutSales(Sale sale)
        {
            if (sale == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry<Sale>(sale).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var result = sale;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao atualizar venda.");
            }

        }

        [HttpDelete]
        [Route("sales")]
        public HttpResponseMessage DeleteSales(int saleId)
        {
            if (saleId < 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Sales.Remove(db.Sales.Find(saleId));
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Venda deletada.");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao deletar venda.");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
