using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_abcash.Models.Entities
{
    public class Facture:BaseModel
    {
        public String Numfact { get; set; }
        public String Refcontrini { get; set; }
        public int Numavenant { get; set; }
        public String Refpds { get; set; }
        public char Etat { get; set; }
        public DateTime DateCalcul { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public int Abontht { get; set; }
        public int Consoht { get; set; }
        public int Prestht { get; set; }
        public int Montantttc { get; set; }
    }
}
