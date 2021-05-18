using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Vluchten_Models;

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
                    .Include(x => x.Reserveringvluchten)
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
                    .Include(x => x.Reserveringvluchten)
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

       public static List<Passagier> PassagiersOphalen()
        {
            using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
            {
                return vluchtenbeheerEntities.Passagier
                    .Include(x => x.Reserveringen.Select(sub => sub.Reserveringvluchten.Select(sub2 => sub2.Vlucht.Luchthaven1)))
                    .Include(x => x.Reserveringen.Select(sub => sub.Reserveringvluchten.Select(sub2 => sub2.Vlucht.Luchthaven)))
                    .OrderBy(x => x.achternaam)
                    .ToList();
            }
        }

        public static List<Reserveringvlucht> AlleVluchten()
        {
            using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
            {
                return vluchtenbeheerEntities.Reserveringvlucht
                    .ToList();
            }
        }

        public static List<Reserveringvlucht> LijstVluchtnummers()
        {
            using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
            {
                return vluchtenbeheerEntities.Reserveringvlucht
                    //.Include(x => x.Vlucht.Luchthaven)
                    .OrderBy(x => x.Vlucht.vertrek)
                    .ToList();
            }
        }

        public static List<Reserveringvlucht> ReserveringIdOphalen(int boekingsreferentie)
        {
            using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
            {
                return vluchtenbeheerEntities.Reserveringvlucht
                    .Include(x => x.Vlucht)
                    .Include(x => x.Reservering)
                    .Where(x => x.reserveringId == boekingsreferentie)
                    .ToList();
            }
        }

        public static List<Reservering> BoekingOphalen()
        {
            using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
            {
                return vluchtenbeheerEntities.Reservering
                    .Include(x => x.Passagier)
                    .Include(x => x.Klasse)
                    .ToList();
            }
        }

        public static int ToevoegenReservatie(Reserveringvlucht reserveringvlucht)
        {
            try
            {
                using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
                {
                    vluchtenbeheerEntities.Reserveringvlucht
                        .Add(reserveringvlucht);
                        return vluchtenbeheerEntities.SaveChanges();
                        
                }
            }
            catch (Exception ex)
            {

                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static List<Passagier> PassagierOphalen()
        {
            using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
                {
                    return vluchtenbeheerEntities.Passagier
                        .Include(x => x.Reserveringen)
                        .ToList();
                }
        }

        public static List<Reservering> PassagierMetNaam(string naam)
        {
            using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
            {
                return vluchtenbeheerEntities.Reservering
                    .Include(x => x.Passagier)
                    .Where(x => x.Passagier.achternaam.Contains(naam))
                    .ToList();
            }
        }
        public static List<Reservering> PassagierMetReservering(int bCode)
        {
            using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
            {
                return vluchtenbeheerEntities.Reservering
                    .Where(x => x.passagierId == bCode)
                    .ToList();
            }
        }
        public static int PassagierToevoegen(Passagier passagier)
        {
            try
            {
                using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
                {
                    vluchtenbeheerEntities.Passagier.Add(passagier);
                    return vluchtenbeheerEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int PassagierVerwijderen(Passagier passagier)
        {
            try
            {
                using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
                {
                    vluchtenbeheerEntities.Entry(passagier).State = EntityState.Deleted;
                    return vluchtenbeheerEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int PassagierAanpassen(Passagier passagier)
        {
            try
            {
                using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
                {
                    vluchtenbeheerEntities.Entry(passagier).State = EntityState.Modified;
                    return vluchtenbeheerEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        /*public static List<Passagier> PassagierMetIdOphalen(int passagierID)
        {
            using (VluchtenbeheerEntities vluchtenbeheerEntities = new VluchtenbeheerEntities())
            {
                return vluchtenbeheerEntities.Passagier
                    .Where(x => x.id == passagierID)
                    .ToList();
            }
        }*/

    }
}
