using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models.MilieuStage
{
    [Table("EntreprisesTypesMilieuxStage", Schema = "dbo")]
    public class EntrepriseTypeMilieuStage
    {
        [Key]
        public int EntrepriseTypeMilieuStageId { get; set; }

        public int EntrepriseId { get; set; }
        public Entreprise Entreprises { get; set; }

        public int TypeMilieuStageId { get; set; }
        public TypeMilieuStage TypesMilieuxStage { get; set; }
    }
}
