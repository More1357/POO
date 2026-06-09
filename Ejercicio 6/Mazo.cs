using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazoDeCartas
{
    public class Mazo
    {
        private List<Carta> mazo = new List<Carta>(48);
        public List<Carta> mazo2 { get { return mazo; } }

        public Mazo()
        {
            string[] palos = { "Oro", "Copa", "Espada", "Basto" };

            int k = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    mazo[k] = new Carta(palos[i], j);
                    k++;
                }
            }
        }

        public Carta? RobarCarta(Mazo mazo)
        {
            if (mazo == null)
            {
                Console.WriteLine($"El mazo está vacío.");
                return null;
            }


            Carta ultCarta = mazo.mazo2[(mazo.mazo2.Count) - 1];
            mazo.mazo2.RemoveAt((mazo.mazo2.Count) - 1);

            return ultCarta;

        }

        int CuantasCartasQuedan(Mazo mazo)
        {
            if (mazo == null)
            {
                Console.WriteLine($"El mazo está vacío.");
                return 0;
            }


            Console.WriteLine($"Quedan {mazo.mazo2.Count} cartas en el mazo.");
            return mazo.mazo2.Count;
        }
    }
}