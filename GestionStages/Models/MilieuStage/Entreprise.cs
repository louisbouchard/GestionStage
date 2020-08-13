using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models.MilieuStage
{
    [Table("Entreprise", Schema = "dbo")]
    public class Entreprise
    {
        [Key]
        public int EntrepriseId { get; set; }

        [StringLength(100)]
        [Required]
        public string NomEntreprise { get; set; }

        [StringLength(300)]
        public string AdresseEntreprise { get; set; }

        public int TypeEntrepriseId { get; set; }

        public TypeEntreprise TypesEntreprise { get; set; }
        public ICollection<EntrepriseTypeMilieuStage> EntreprisesTypesMilieuxStage { get; set; }
    }
}
