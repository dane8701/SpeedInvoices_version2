using SpeedInvoices.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpeedInvoices.Service.Controllers
{
    public class FactureController : BaseController
    {
        [HttpPost]
        public IHttpActionResult AjouterFacture([FromBody] Facture model)
        {
            try
            {
                var factures = db.Factures.ToList();

                Facture facture = new Facture
                {
                    Id = model.Id,
                    Reference = "R" + factures.Count(),
                    DateCreation = DateTime.Now.Date,
                    Remise = model.Remise,
                    Tva = model.Tva,
                    MontantHt = model.MontantHt,
                    MontantTtc = model.MontantTtc,
                    IdEtat = model.IdEtat,
                    IdClient = model.IdClient,
                    IdStructure = model.IdStructure
                };

                facture.Id = 0;
                facture.DateCreation = DateTime.Now;
                db.Factures.Add(facture);
                db.SaveChanges();

                
                Facture lastFacture = new Facture();
                lastFacture.Id = -1;
                foreach (var item in factures)
                {
                    if (lastFacture.Id < item.Id)
                        lastFacture = item;
                }

                foreach (var p in model.Prestations)
                {
                    db.Prestations.Add(
                        new Prestation 
                        { 
                            IdFacture = (int)lastFacture.Id, 
                            Intitule = p.Intitule, 
                            Description = p.Description,
                            PrixUnitaire = p.PrixUnitaire,
                            Quantite = p.Quantite,
                            PrixTotal = p.PrixTotal
                        }
                    );
                }
                db.SaveChanges();
                return Ok(facture);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ListerFacture(int IdStructure, int IdEtat)
        {
            try
            {
                var factures = db.Factures.Where(x => x.IdStructure == IdStructure && x.IdEtat == IdEtat)
                    .OrderBy(x => x.DateCreationFacture).ToList();
                return Ok(factures);
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
        public IHttpActionResult ListerFactureClient(int IdClient, int IdEtat)
        {
            try
            {
                var factures = db.Factures.Where(x => x.IdClient == IdClient && x.IdEtat == IdEtat)
                    .OrderBy(x => x.DateCreationFacture).ToList();
                return Ok(factures);
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
        public IHttpActionResult DetailsFacture(int IdStructure, int id)
        {
            try
            {
                var facture = db.Factures.Where(x => x.IdStructure == IdStructure && x.IdFacture == id);
                return Ok(facture);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult DetailsFactureClient(int IdClient, int id)
        {
            try
            {
                var facture = db.Factures.Where(x => x.IdClient == IdClient && x.IdFacture == id);
                return Ok(facture);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult SupprimerFacture(int id)
        {
            try
            {
                var facture = db.Factures.Find(id);
                if (facture == null)
                    return Content(HttpStatusCode.NotFound, "La facture " + id + " n'existe pas.");
                db.Factures.Remove(facture);
                db.SaveChanges();

                var produits = db.Produit_Facture.Where(x => x.IdFacture == id).ToList();
                db.Produit_Facture.RemoveRange(produits);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
