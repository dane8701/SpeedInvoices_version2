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
    
    public partial class Facture
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Facture()
        {
            this.Prestations = new HashSet<Prestation>();
        }
    
        public int Id { get; set; }
        public string Reference { get; set; }
        public System.DateTime DateCreation { get; set; }
        public int Remise { get; set; }
        public float Tva { get; set; }
        public float MontantHt { get; set; }
        public float MontantTtc { get; set; }
        public int IdEtat { get; set; }
        public int IdClient { get; set; }
        public int IdStructure { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Etat Etat { get; set; }
        public virtual Structure Structure { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestation> Prestations { get; set; }
    }
}
