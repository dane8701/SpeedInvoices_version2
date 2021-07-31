using SpeedInvoices.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpeedInvoices.Service.Controllers
{
    public class ClientController : BaseController
    {
        [HttpPost]
        public IHttpActionResult AjouterClient([FromBody] Client client)
        {
            try
            {
                client.Id = 0;
                db.Clients.Add(client);
                db.SaveChanges();
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ListerClient()
        {
            try
            {
                var clients = db.Clients
                    .OrderBy(x => x.Nom).ToList();
                return Ok(clients);
            }
            catch (DbUpdateException ex)
            {
                var exception = ex.InnerException?.InnerException as SqlException;
                return BadRequest(exception?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult DetailsClient(int Id)
        {
            try
            {
                var client = db.Clients.Where(x => x.Id == Id);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult SupprimerClient(int Id)
        {
            try
            {
                var client = db.Clients.Find(Id);
                if (client == null)
                    return Content(HttpStatusCode.NotFound, "Ce client n'existe pas.");
                db.Clients.Remove(client);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult EditerClient([FromBody] Client newClient)
        {
            try
            {
                var oldClient = db.Clients.AsNoTracking().FirstOrDefault(x => x.Id == newClient.Id);
                if (oldClient == null)
                    return Content(HttpStatusCode.NotFound, "Ce client n'existe pas.");
                db.Entry(newClient).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok(newClient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
