using GestionStages.Models.MilieuStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.ViewModels.MilieuStage
{
    public class EntrepriseModel
    {
        public Entreprise Entreprises { get; set; }

        public ICollection<EntrepriseTypeMilieuStage> EntreprisesTypesMilieuxStage { get; set; }
    }
}
