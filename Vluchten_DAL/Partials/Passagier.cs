using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vluchten_Models;

namespace Vluchten_DAL
{
    public partial class Passagier : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "voornaam" && string.IsNullOrWhiteSpace(voornaam))
                {
                    return "Voornaam is vereist!" + Environment.NewLine;
                }
                if (columnName == "achternaam" && string.IsNullOrWhiteSpace(achternaam))
                {
                    return "Achternaam is vereist!" + Environment.NewLine;
                }
                if (columnName == "emailadres" && string.IsNullOrWhiteSpace(emailadres))
                {
                    return "Emailadres is vereist!" + Environment.NewLine;

                }
                if (columnName == "telefoonnummer" && string.IsNullOrWhiteSpace(telefoonnummer))
                {
                    return "Telefoonnummer is vereist!" + Environment.NewLine;
                }
                if (columnName == "nationaliteit" && string.IsNullOrWhiteSpace(nationaliteit))
                {
                    return "Uw nationaliteit is vereist!" + Environment.NewLine;
                }
                if (columnName == "plaats" && string.IsNullOrWhiteSpace(plaats))
                {
                    return "De plaats is vereist!" + Environment.NewLine;
                }
                if (columnName == "id" && id <= 0)
                {
                    return "Het id moet een positief getal zijn!" + Environment.NewLine;
                }
                return "";
            }
        }
    }
}
