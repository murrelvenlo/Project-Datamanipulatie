using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Vluchten_DAL
{
    public static class DatabaseOperations
    {
        public static List<Vlucht> VluchtenZoeken()
        {
            using (VluchtenbeheerEntities flightsEntities = new VluchtenbeheerEntities())
            {
                return flightsEntities.Vlucht
                    .Include(x => x.Luchthaven)
                    .Include(x => x.Luchthaven1)
                    .ToList();
            }
        }

        //Filter op basis van Luchthaven
        public static List<Vlucht> GewensteVluchtenZoeken(string heen, string aankomst)
        {
            using (VluchtenbeheerEntities flightsEntities = new VluchtenbeheerEntities())
            {
                return flightsEntities.Vlucht
                    .Include(x => x.Luchthaven)
                    .Include(x => x.Luchthaven1)
                    .Where(x => x.Luchthaven1.stad.Contains(heen) && x.Luchthaven.stad.Contains(aankomst))
                    .ToList();
            }
        }

        public static List<Klasse> OphalenKlasse()
        {
            using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
            {
                return vluchtenbeheerEntities.Klasse
                    .OrderBy(x => x.klasseType)
                    .ToList();
            }
        }

       // public static List<Luchthaven> VluchtInformatie()
        //{
           // using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
            //{
               // return vluchtenbeheerEntities.Vlucht
                   // .Include(x => x.Luchthaven1)
                    //.Include(x => x.Luchthaven)

                    //.ToList();
            //}
       // }

        
    }
}
