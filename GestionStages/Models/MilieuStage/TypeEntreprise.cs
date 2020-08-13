using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models.MilieuStage
{
    [Table("TypesEntreprises", Schema = "dbo")]
    public class TypeEntreprise
    {
        [Key]
        public int TypeEntrepriseId { get; set; }

        [StringLength(100)]
        [Required]
        public string DescriptionTypeEntreprise { get; set; }

        public ICollection<Entreprise> Entreprise { get; set; }
    }
}
