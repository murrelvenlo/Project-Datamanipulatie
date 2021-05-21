using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vluchten_Models
{
    public static class Conversies
    {
        public static string DateTimeConverterenNaarDate(DateTime dag)
        {
            return dag.ToString("D");
        }

        public static string Valuta = "€";
    }
}
