using SpeedInvoices.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpeedInvoices.Service.Controllers
{
    public class PrestationController : BaseController
    {
        [HttpPost]
        public IHttpActionResult AjouterPrestation([FromBody] Prestation prestation)
        {
            try
            {
                prestation.Id = 0;
                db.Prestations.Add(prestation);
                db.SaveChanges();
                return Ok(prestation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> ListerPrestation()
        {
            try
            {
                var Prestations = await db.Prestations
                    .OrderBy(x => x.Intitule).ToArrayAsync();
                return Ok(Prestations);
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
        public IHttpActionResult DetailsPrestation(int Id)
        {
            try
            {
                var Prestation = db.Prestations.Where(x => x.Id == Id);
                return Ok(Prestation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult SupprimerPrestation(int Id)
        {
            try
            {
                var Prestation = db.Prestations.Find(Id);
                if (Prestation == null)
                    return Content(HttpStatusCode.NotFound, "Cette prestation n'existe pas.");
                db.Prestations.Remove(Prestation);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult EditerPrestation([FromBody] Prestation newPrestation)
        {
            try
            {
                var oldPrestation = db.Prestations.AsNoTracking().FirstOrDefault(x => x.Id == newPrestation.Id);
                if (oldPrestation == null)
                    return Content(HttpStatusCode.NotFound, "Cette prestation n'existe pas.");
                db.Entry(newPrestation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok(newPrestation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
