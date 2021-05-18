using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vluchten_Models;

namespace Vluchten_DAL
{

    public partial class Reserveringvlucht : BasisKlasse
    {
        /*public override bool Equals(object obj)
        {
            return obj is Reserveringvlucht reserveringvlucht &&
                   reserveringId == reserveringvlucht.reserveringId;
        }

        public override int GetHashCode()
        {
            return -2094072504 + reserveringId.GetHashCode();
        }

        public override string ToString()
        {
            return this.vluchtId;
        }*/

        public override string this[string columnName] 
        { get {
                if (columnName == "reserveringId" && reserveringId <= 0)
                {
                    return "Boekingsreferentie moet groter zijn dan 0!";
                }
                if (columnName == "vluchtId" && !string.IsNullOrWhiteSpace(vluchtId))
                {
                    return "Het vluchtnummer moet ingevuld zijn!";
                }
                return "";
               }
        }


    }

    
}
