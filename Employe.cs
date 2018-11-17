using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulImpot
{
    class Employe
    {
        public int Id { get; set; }
        public string prenom { get; set; }
        public string nom { get; set; }
        public double salaire { get; set; }
        public int nbreJours { get; set; }
        public int conjoint { get; set; }
        public int enfants { get; set; }
        public double brutFiscaAnnuel { get; set; }
        public double brutFiscal { get; set; }
        public double abattement { get; set; }
        public double irpp { get; set; }
        public double nbreParts { get; set; }
        public double reduction { get; set; }
        public double impots { get; set; }

        public Employe() { }
    }
}
