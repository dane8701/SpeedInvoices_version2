//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpeedInvoices.Service.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Prestation
    {
        public int Id { get; set; }
        public string Intitule { get; set; }
        public string Description { get; set; }
        public int PrixUnitaire { get; set; }
        public int Quantite { get; set; }
        public int PrixTotal { get; set; }
        public int IdFacture { get; set; }
    
        public virtual Facture Facture { get; set; }
    }
}
