using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models.MilieuStage
{
    [Table("TypesMilieuxStage", Schema = "dbo")]
    public class TypeMilieuStage
    {
        [Key]
        public int TypeMilieuStageId { get; set; }

        [StringLength(100)]
        [Required]
        public string DescriptionTypeMilieuStage { get; set; }

        public ICollection<EntrepriseTypeMilieuStage> EntreprisesTypesMilieuxStage { get; set; }
    }
}
